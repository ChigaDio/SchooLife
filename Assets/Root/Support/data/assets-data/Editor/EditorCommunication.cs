using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using System.Linq;

public class EditorCommunication : EditorWindow
{
    private static TcpListener listener;
    private static Thread listenerThread;
    private static volatile bool pendingCommand;
    private static string pendingCommandName;
    private static CommData pendingCommandData;
    private static string commandResult;

    // ウィンドウを表示するためのメニュー項目を追加
    [MenuItem("Window/Communication Server")]
    public static void ShowWindow()
    {
        GetWindow<EditorCommunication>("Comm Server");
    }

    // JsonUtility は List を直列化できないのでラッパーが必要
    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> items;
    }

    [MenuItem("Tools/通信サーバー開始")]
    public static void StartServer()
    {
        if (listener != null) return;
        listener = new TcpListener(IPAddress.Loopback, 12345);
        listener.Start();
        listenerThread = new Thread(new ThreadStart(ListenForClients));
        listenerThread.IsBackground = true;
        listenerThread.Start();
        EditorApplication.update += ProcessPendingCommand;
        Debug.Log("通信サーバーを開始しました。");
    }

    [MenuItem("Tools/通信サーバー停止")]
    public static void StopServer()
    {
        listener?.Stop();
        listener = null;
        EditorApplication.update -= ProcessPendingCommand;
        Debug.Log("通信サーバーを停止しました。");
    }

    // EditorウィンドウのGUIを描画
    private void OnGUI()
    {
        GUILayout.Label("Communication Server Status", EditorStyles.boldLabel);

        // サーバー状態に応じてインジケーターの色を設定
        Color indicatorColor = listener != null ? Color.green : Color.red;
        string statusText = listener != null ? "Running" : "Stopped";

        // インジケーターの描画
        Rect indicatorRect = GUILayoutUtility.GetRect(20, 20);
        EditorGUI.DrawRect(indicatorRect, indicatorColor);

        // ステータステキストの表示
        GUILayout.Label($"Status: {statusText}", EditorStyles.label);

        // サーバー開始/停止ボタン
        if (listener == null)
        {
            if (GUILayout.Button("Start Server"))
            {
                StartServer();
            }
        }
        else
        {
            if (GUILayout.Button("Stop Server"))
            {
                StopServer();
            }
        }
    }

    private static void ListenForClients()
    {
        while (true)
        {
            try
            {
                using (TcpClient client = listener.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] lenBytes = new byte[4];
                    stream.Read(lenBytes, 0, 4);
                    int len = BitConverter.ToInt32(lenBytes, 0);
                    byte[] msgBytes = new byte[len];
                    stream.Read(msgBytes, 0, len);
                    string msg = System.Text.Encoding.UTF8.GetString(msgBytes);
                    var json = JsonUtility.FromJson<CommMessage>(msg);

                    pendingCommandName = json.command;
                    pendingCommandData = json.data;
                    pendingCommand = true;

                    while (pendingCommand)
                    {
                        Thread.Sleep(10);
                    }

                    var response = new CommMessage { result = commandResult };
                    byte[] respBytes = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(response));
                    stream.Write(BitConverter.GetBytes(respBytes.Length), 0, 4);
                    stream.Write(respBytes, 0, respBytes.Length);
                }
            }
            catch (Exception e)
            {
                if (listener == null) break;
                Debug.LogError(e);
            }
        }
    }

    private static void ProcessPendingCommand()
    {
        if (!pendingCommand) return;
        commandResult = HandleCommand(pendingCommandName, pendingCommandData);
        pendingCommand = false;
    }

    private static string HandleCommand(string command, CommData data)
    {
        if (command == "get_project_path")
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        }
        else if (command == "get_addressable_path")
        {
            string filePath = data.file_path;
            Debug.Log($"Received filePath: {filePath}");

            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace("\\", "/").TrimEnd('/');
            Debug.Log($"Project root (normalized): {projectRoot}");

            string normalizedFilePath = filePath.Replace("\\", "/").TrimEnd('/');
            Debug.Log($"Normalized filePath: {normalizedFilePath}");

            string assetPath = normalizedFilePath;
            if (normalizedFilePath.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase))
            {
                assetPath = normalizedFilePath.Substring(projectRoot.Length).TrimStart('/');
                Debug.Log($"Trimmed assetPath: {assetPath}");
            }
            else
            {
                Debug.LogWarning($"filePath does not start with project root: {projectRoot}");
            }

            assetPath = assetPath.Replace("\\", "/").Trim();
            if (!assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
            {
                assetPath = "Assets/" + assetPath.TrimStart('/');
            }
            Debug.Log($"Final assetPath: {assetPath}");

            string fullPath = Path.Combine(projectRoot, assetPath).Replace("\\", "/");
            if (!AssetDatabase.IsValidFolder(Path.GetDirectoryName(assetPath)) && !File.Exists(fullPath))
            {
                Debug.LogWarning($"Invalid asset path for AssetDatabase: {assetPath}");
                return assetPath;
            }

            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            Debug.Log($"GUID: {guid}");

            if (string.IsNullOrEmpty(guid) || guid == "00000000000000000000000000000000")
            {
                Debug.LogWarning($"No valid GUID found for assetPath: {assetPath}");
                return assetPath;
            }

            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                Debug.LogWarning("AddressableAssetSettings is not initialized.");
                return assetPath;
            }

            var entry = settings.FindAssetEntry(guid);
            if (entry != null)
            {
                Debug.Log($"Found Addressable entry: {entry.address}");
                return entry.address;
            }
            else
            {
                Debug.LogWarning($"Asset not Addressable: {assetPath}. Returning relative path.");
                return assetPath;
            }
        }
        else if (command == "get_sprite_info")
        {
            string filePath = data.file_path;
            string assetPath = filePath.Replace("\\", "/");
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace("\\", "/").TrimEnd('/');
            if (assetPath.StartsWith(projectRoot, StringComparison.OrdinalIgnoreCase))
            {
                assetPath = assetPath.Substring(projectRoot.Length).TrimStart('/');
            }
            if (!assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
            {
                assetPath = "Assets/" + assetPath.TrimStart('/');
            }

            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer == null || importer.spriteImportMode != SpriteImportMode.Multiple)
            {
                return "[]";
            }

            var assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            var sprites = new List<Sprite>();  // Sprite型でリストにする
            foreach (var obj in assets)
            {
                if (obj is Sprite sprite)
                {
                    sprites.Add(sprite);
                }
            }

            // 名前から数値部分を抽出してソート（例: "sprite_10" の "10" をintに変換）
            sprites.Sort((a, b) =>
            {
                int numA = int.Parse(a.name.Split('_').Last());  // 名前が "sprite_数字" 形式の場合
                int numB = int.Parse(b.name.Split('_').Last());
                return numA.CompareTo(numB);
            });

            // 名前リストにする場合
            var spriteNames = sprites.Select(s => s.name).ToList();

            return JsonUtility.ToJson(new Wrapper<string> { items = spriteNames });
        }
        return null;
    }

    [System.Serializable]
    private class CommMessage
    {
        public string command;
        public CommData data;
        public string result;
    }

    [System.Serializable]
    private class CommData
    {
        public string file_path;
    }
}
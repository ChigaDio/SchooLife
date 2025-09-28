using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class EditorCommunication : EditorWindow
{
    private static TcpListener listener;
    private static Thread listenerThread;
    private static volatile bool pendingCommand;
    private static string pendingCommandName;
    private static CommData pendingCommandData; // 修正: string から CommData に変更
    private static string commandResult;

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

                    // メインスレッドで処理するためにコマンドを保存
                    pendingCommandName = json.command;
                    pendingCommandData = json.data; // 修正: string から CommData に変更
                    pendingCommand = true;

                    // メインスレッドが処理を終えるまで待機
                    while (pendingCommand)
                    {
                        Thread.Sleep(10);
                    }

                    // レスポンスを送信
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

        // メインスレッドでコマンドを処理
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

            // プロジェクトルートを取得
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace("\\", "/").TrimEnd('/');
            Debug.Log($"Project root (normalized): {projectRoot}");

            // filePath を正規化（スラッシュを統一）
            string normalizedFilePath = filePath.Replace("\\", "/").TrimEnd('/');
            Debug.Log($"Normalized filePath: {normalizedFilePath}");

            // 相対パスを計算
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

            // パスを Unity の形式（Assets/ 始まり）に統一
            assetPath = assetPath.Replace("\\", "/").Trim();
            if (!assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
            {
                assetPath = "Assets/" + assetPath.TrimStart('/');
            }
            Debug.Log($"Final assetPath: {assetPath}");

            // AssetDatabase でパスを検証
            string fullPath = Path.Combine(projectRoot, assetPath).Replace("\\", "/");
            if (!AssetDatabase.IsValidFolder(Path.GetDirectoryName(assetPath)) && !File.Exists(fullPath))
            {
                Debug.LogWarning($"Invalid asset path for AssetDatabase: {assetPath}");
                return assetPath; // 無効な場合は相対パスを返す
            }

            // GUID を取得
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            Debug.Log($"GUID: {guid}");

            if (string.IsNullOrEmpty(guid) || guid == "00000000000000000000000000000000")
            {
                Debug.LogWarning($"No valid GUID found for assetPath: {assetPath}");
                return assetPath; // 相対パスを返す
            }

            // Addressable 設定からエントリを検索
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
        return null;
    }

    [System.Serializable]
    private class CommMessage
    {
        public string command;
        public CommData data; // 修正: string から CommData に変更
        public string result;
    }

    [System.Serializable]
    private class CommData
    {
        public string file_path; // JSON の "file_path" に対応
    }
}
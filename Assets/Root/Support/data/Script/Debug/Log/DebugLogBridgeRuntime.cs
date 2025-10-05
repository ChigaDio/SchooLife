using System;
using System.Net.WebSockets;
using UnityEngine;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;

public class DebugLogBridgeRuntime : MonoBehaviour
{
    private WebSocket ws;
    private const string WebSocketUrl = "ws://localhost:8765"; // Python WebSocket�T�[�o�[��URL
    private float reconnectTimer;
    private bool isConnecting;

    void Awake()
    {
        TryConnect();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // �Đڑ��Ď�
        reconnectTimer += Time.deltaTime;
        if (reconnectTimer > 5f)
        {
            reconnectTimer = 0f;
            if (ws == null || ws.ReadyState != WebSocketSharp.WebSocketState.Open)
            {
                TryConnect();
            }
        }
    }

    private void TryConnect()
    {
        if (isConnecting) return; // �ڑ����s���̏d���h�~
        isConnecting = true;

        try
        {
            // �����̐ڑ������
            ws?.Close();

            // �V����WebSocket���쐬
            ws = new WebSocket(WebSocketUrl);

            // �C�x���g�n���h����ݒ�i���C���X���b�h�Ŏ��s�j
            ws.OnOpen += (sender, e) =>
            {
                UnityEngine.Debug.Log("WebSocket connected successfully!");
            };

            ws.OnError += (sender, e) =>
            {
                UnityEngine.Debug.LogWarning($"WebSocket error: {e.Message}");
                ws = null; // �Đڑ����g���K�[
            };

            ws.OnClose += (sender, e) =>
            {
                UnityEngine.Debug.Log($"WebSocket disconnected. Reason: {e.Reason}");
                ws = null; // �Đڑ����g���K�[
            };

            // �I�v�V����: �T�[�o�[����̃��b�Z�[�W��M�i�K�v�ɉ����ėL�����j
            // ws.OnMessage += (sender, e) =>
            // {
            //     UnityEngine.Debug.Log($"Received message: {e.Data}");
            // };

            // �񓯊��ڑ������s
            ws.ConnectAsync();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning($"DebugBridge WebSocket connection failed: {e.Message}");
            ws = null;
        }
        finally
        {
            isConnecting = false;
        }
    }

    public void SendLog(string message, string type)
    {
        if (ws == null || ws.ReadyState != WebSocketSharp.WebSocketState.Open)
        {
            UnityEngine.Debug.LogWarning("WebSocket not connected. Skipping send.");
            return;
        }

        var json = JsonUtility.ToJson(new LogData
        {
            message = message,
            type = type,
            time = DateTime.Now.ToString("HH:mm:ss")
        });

        try
        {
            ws.Send(json);
            UnityEngine.Debug.Log($"Sent log: [{type}] {message}");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning($"Failed to send log: {e.Message}");
            ws = null; // �Đڑ����g���K�[
        }
    }

    [Serializable]
    private class LogData
    {
        public string message;
        public string type;
        public string time;
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
            ws = null;
        }
    }
}
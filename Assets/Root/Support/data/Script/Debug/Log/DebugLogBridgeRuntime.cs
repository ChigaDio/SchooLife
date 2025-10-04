using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class DebugLogBridgeRuntime : MonoBehaviour
{
    private UdpClient client;
    private IPEndPoint endpoint;
    private const int Port = 41234;
    private float reconnectTimer;

    void Awake()
    {
        TryConnect();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // çƒê⁄ë±äƒéã
        reconnectTimer += Time.deltaTime;
        if (reconnectTimer > 5f)
        {
            reconnectTimer = 0f;
            if (client == null)
                TryConnect();
        }
    }

    private void TryConnect()
    {
        try
        {
            client?.Close();
            client = new UdpClient();
            endpoint = new IPEndPoint(IPAddress.Loopback, Port);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"DebugBridge connection failed: {e.Message}");
            client = null;
        }
    }

    public void SendLog(string message, string type)
    {
        if (client == null) return;

        var json = JsonUtility.ToJson(new LogData
        {
            message = message,
            type = type,
            time = DateTime.Now.ToString("HH:mm:ss")
        });
        var data = Encoding.UTF8.GetBytes(json);
        try
        {
            client.Send(data, data.Length, endpoint);
        }
        catch
        {
            client = null; // çƒê⁄ë±Ç≥ÇπÇÈ
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
        client?.Close();
    }
}

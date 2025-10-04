using System.Diagnostics;
using UnityEngine;

/// <summary>
/// デバッグ汎用関数
/// </summary>
public static class DebugLogBridge
{
    private static DebugLogBridgeRuntime runtime;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (UnityEngine.Debug.isDebugBuild || Application.isEditor)
        {
            var go = new GameObject("DebugBridge");
            Object.DontDestroyOnLoad(go);
            runtime = go.AddComponent<DebugLogBridgeRuntime>();
        }
    }

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
        runtime?.SendLog(message, "Log");
    }

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void LogWarning(string message)
    {
        UnityEngine.Debug.LogWarning(message);
        runtime?.SendLog(message, "Warning");
    }

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void LogError(string message)
    {
        UnityEngine.Debug.LogError(message);
        runtime?.SendLog(message, "Error");
    }
}

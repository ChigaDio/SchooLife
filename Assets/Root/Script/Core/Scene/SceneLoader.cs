using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using GameCore;

public enum GameScene
{
    Title,
    Main,
    PlayerSetting,
    GamePlay
}

public class SceneLoader
{
    // シーン名管理
    public static readonly Dictionary<GameScene, string> sceneNames = new Dictionary<GameScene, string>
    {
        { GameScene.Title, "TitleScene" },
        { GameScene.Main, "MainScene" },
        {GameScene.PlayerSetting,"PlayerInitScene" },
        {GameScene.GamePlay,"GameScene" }

    };

    private static readonly HashSet<GameScene> loadedScenes = new HashSet<GameScene>();
    private static readonly Dictionary<GameScene, UniTask> loadingTasks = new Dictionary<GameScene, UniTask>();



    #region Load / Unload

    public static async UniTask LoadSceneAsync(GameScene scene, bool additive = false, Action action = null)
    {
        if (loadedScenes.Contains(scene))
        {
            DebugLog($"Scene '{scene}' is already loaded.");
            return;
        }

        if (loadingTasks.TryGetValue(scene, out UniTask existingTask))
        {
            DebugLog($"Scene '{scene}' is already loading, waiting...");
            await existingTask;
            return;
        }

        var task = InternalLoadSceneAsync(scene, additive,action);
        loadingTasks.Add(scene, task);

        try
        {
            await task;
        }
        finally
        {
            loadingTasks.Remove(scene);
        }
    }

    private static async UniTask InternalLoadSceneAsync(GameScene scene, bool additive,Action action = null)
    {
        if (!sceneNames.TryGetValue(scene, out string sceneName))
        {
            Debug.LogError($"Scene enum '{scene}' is not mapped to a scene name.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' does not exist in build settings.");
            return;
        }

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        asyncOp.allowSceneActivation = true;

        while (!asyncOp.isDone)
            await UniTask.Yield();

        loadedScenes.Add(scene);
        action?.Invoke();
        DebugLog($"Scene '{scene}' loaded successfully.");
    }

    public static async UniTask UnloadSceneAsync(GameScene scene, Action action = null)
    {
        if (!loadedScenes.Contains(scene))
        {
            DebugLog($"Scene '{scene}' is not loaded, cannot unload.");
            return;
        }

        if (!sceneNames.TryGetValue(scene, out string sceneName))
        {
            Debug.LogError($"Scene enum '{scene}' is not mapped to a scene name.");
            return;
        }

        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(sceneName);
        if (asyncOp == null)
        {
            Debug.LogError($"Failed to unload scene '{sceneName}'.");
            return;
        }

        while (!asyncOp.isDone)
            await UniTask.Yield();

        loadedScenes.Remove(scene);
        GC.Collect();
        await Resources.UnloadUnusedAssets();
        action?.Invoke();   
        DebugLog($"Scene '{scene}' unloaded successfully.");
    }

    /// <summary>
    /// 現在ロード済みのシーンをすべてアンロード
    /// </summary>
    /// <param name="keepScenes">残したいシーン</param>
    public static async UniTask UnloadAllScenesAsync(GameScene[] keepScenes,Action action = null)
    {
        var toKeep = new HashSet<GameScene>(keepScenes);
        var toUnload = new List<GameScene>();

        foreach (var scene in loadedScenes)
            if (!toKeep.Contains(scene))
                toUnload.Add(scene);

        foreach (var scene in toUnload)
            await UnloadSceneAsync(scene);

        action?.Invoke();
    }

    #endregion

    #region Instantiate in Scene

    /// <summary>
    /// 指定シーンに GameObject を生成
    /// </summary>
    public static GameObject InstantiateInScene(GameObject prefab, GameScene scene)
    {
        if (!sceneNames.TryGetValue(scene, out string sceneName))
        {
            Debug.LogError($"Scene enum '{scene}' is not mapped to a scene name.");
            return null;
        }

        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        if (!targetScene.isLoaded)
        {
            Debug.LogError($"Scene '{sceneName}' is not loaded.");
            return null;
        }

        GameObject obj = GameObject.Instantiate(prefab);
        SceneManager.MoveGameObjectToScene(obj, targetScene); // 安全に所属させる
        return obj;
    }

    #endregion

    #region Utility

    public static IReadOnlyCollection<GameScene> GetLoadedScenes() => loadedScenes;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private static void DebugLog(string message) => Debug.Log("[SceneLoader] " + message);

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void RegisterActiveScene()
    {
        var active = SceneManager.GetActiveScene();
        foreach (var kv in sceneNames)
        {
            if (kv.Value == active.name)
            {
                loadedScenes.Add(kv.Key);
                break;
            }
        }
    }

    #endregion
}
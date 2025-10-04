using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using static UnityEngine.Rendering.HDROutputUtils;

public class HermiteUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject uiPrefab;  // UIプレハブ

    private List<HermiteUIObject> activeObjects = new List<HermiteUIObject>();
    private CancellationTokenSource cancellationTokenSource;

    private void Awake()
    {
        cancellationTokenSource = new CancellationTokenSource();
    }

    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
        cancellationTokenSource = null;
    }

    /// <summary>
    /// 非同期でHermiteUIオブジェクトを生成して移動
    /// </summary>
    public async UniTask<HermiteUIObject[]> CreateAsync(
        Vector3 start,
        Vector3 target,
        string[] text,
        float duration = 1.5f,
        Action<HermiteUIObject> onEachArrived = null,
        Action onAllCompleted = null)
    {

        if (uiPrefab == null)
        {
            Debug.LogError("uiPrefabが設定されていません。");
            return null;
        }

        if (cancellationTokenSource.IsCancellationRequested)
        {
            Debug.LogWarning("キャンセルトークンがキャンセル済みです。");
            return null;
        }

        // 非同期インスタンス生成
        AsyncInstantiateOperation<GameObject> handler = InstantiateAsync(uiPrefab, text.Length, transform);

        // インスタンス生成の完了を待つ
        // 生成が完了するまで待つ
        while (!handler.isDone)
        {
            await UniTask.Yield();
        }


        // HermiteUIObjectをメインスレッドで生成
        var hermiteObjects = new List<HermiteUIObject>(text.Length);
        for (int i = 0; i < text.Length; i++)
        {
            hermiteObjects.Add(new HermiteUIObject(handler.Result[i], start, target, text[i]));
        }

        // アクティブリストに追加
        activeObjects.AddRange(hermiteObjects);

        // 各オブジェクトの移動と破棄を非同期で開始
        var tasks = new List<UniTask>(hermiteObjects.Count);
        foreach (var hermiteObj in hermiteObjects)
        {
            var task = MoveWithCallbackAsync(hermiteObj, onEachArrived, cancellationTokenSource.Token);
            tasks.Add(task);
        }

        // すべてのタスクの完了を待つ
        try
        {
            await UniTask.WhenAll(tasks).AttachExternalCancellation(cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("移動がキャンセルされました。");
            return hermiteObjects.ToArray();
        }

        // すべてのオブジェクトが完了したらコールバック
        onAllCompleted?.Invoke();

        // 完了後にアクティブリストから削除
        activeObjects.RemoveAll(obj => !obj.IsActive);

        return hermiteObjects.ToArray();
    }

    private async UniTask MoveWithCallbackAsync(
        HermiteUIObject hermiteObj,
        Action<HermiteUIObject> onEachArrived,
        CancellationToken cancellationToken)
    {
        try
        {
            await hermiteObj.MoveAndDestroyAsync(cancellationToken: cancellationToken);
            onEachArrived?.Invoke(hermiteObj);
        }
        catch (OperationCanceledException)
        {
            Debug.Log($"移動がキャンセルされました: {hermiteObj}");
        }
    }
}
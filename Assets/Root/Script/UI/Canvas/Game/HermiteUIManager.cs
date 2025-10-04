using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using static UnityEngine.Rendering.HDROutputUtils;

public class HermiteUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject uiPrefab;  // UI�v���n�u

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
    /// �񓯊���HermiteUI�I�u�W�F�N�g�𐶐����Ĉړ�
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
            Debug.LogError("uiPrefab���ݒ肳��Ă��܂���B");
            return null;
        }

        if (cancellationTokenSource.IsCancellationRequested)
        {
            Debug.LogWarning("�L�����Z���g�[�N�����L�����Z���ς݂ł��B");
            return null;
        }

        // �񓯊��C���X�^���X����
        AsyncInstantiateOperation<GameObject> handler = InstantiateAsync(uiPrefab, text.Length, transform);

        // �C���X�^���X�����̊�����҂�
        // ��������������܂ő҂�
        while (!handler.isDone)
        {
            await UniTask.Yield();
        }


        // HermiteUIObject�����C���X���b�h�Ő���
        var hermiteObjects = new List<HermiteUIObject>(text.Length);
        for (int i = 0; i < text.Length; i++)
        {
            hermiteObjects.Add(new HermiteUIObject(handler.Result[i], start, target, text[i]));
        }

        // �A�N�e�B�u���X�g�ɒǉ�
        activeObjects.AddRange(hermiteObjects);

        // �e�I�u�W�F�N�g�̈ړ��Ɣj����񓯊��ŊJ�n
        var tasks = new List<UniTask>(hermiteObjects.Count);
        foreach (var hermiteObj in hermiteObjects)
        {
            var task = MoveWithCallbackAsync(hermiteObj, onEachArrived, cancellationTokenSource.Token);
            tasks.Add(task);
        }

        // ���ׂẴ^�X�N�̊�����҂�
        try
        {
            await UniTask.WhenAll(tasks).AttachExternalCancellation(cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("�ړ����L�����Z������܂����B");
            return hermiteObjects.ToArray();
        }

        // ���ׂẴI�u�W�F�N�g������������R�[���o�b�N
        onAllCompleted?.Invoke();

        // ������ɃA�N�e�B�u���X�g����폜
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
            Debug.Log($"�ړ����L�����Z������܂���: {hermiteObj}");
        }
    }
}
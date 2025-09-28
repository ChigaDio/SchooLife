using Cysharp.Threading.Tasks;
using GameCore;
using GameCore.Tables;
using System.IO;
using System.Threading;
using System;
using UnityEngine;

public class ClassDataIDCore : BaseSingleton<ClassDataIDCore>
{
    private ClassDataHeader m_classDataTables;
    private CancellationToken cts;
    private bool isLoaded;

    public override void AwakeSingleton()
    {
        base.AwakeSingleton();
        instance = this;
        if (cts == null) cts = this.GetCancellationTokenOnDestroy();
        isLoaded = false;
        DontDestroyOnLoad(instance);
    }


    private void OnDestroy()
    {

    }

    /// <summary>
    /// all_class_data.bin を読み込み、BinaryReader をラムダに渡して実行
    /// </summary>
    public async UniTask LoadClassDataAsync(Func<BinaryReader, ClassDataHeader, UniTask> onLoaded)
    {
        if (cts == null) cts = this.GetCancellationTokenOnDestroy();
        if (isLoaded) return;


        string path = SupportFiles.ALL_ID_BIN;



        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                if (m_classDataTables == null) m_classDataTables = new ClassDataHeader(reader);
                if (onLoaded != null)
                {
                    // スレッド切り替えを内部で処理
                    await ExecuteOnThreadPoolAndReturn(onLoaded, reader, m_classDataTables, cts);
                }
                isLoaded = true;
            }
        }
        catch (OperationCanceledException)
        {
            Debug.LogWarning("TableIDCoreの読み込みがキャンセルされました。");
        }
        catch (Exception ex)
        {
            Debug.LogError($"読み込み中にエラーが発生: {ex}");
        }
    }

    private async UniTask ExecuteOnThreadPoolAndReturn(
    Func<BinaryReader, ClassDataHeader, UniTask> action,
    BinaryReader reader,
    ClassDataHeader classDataHeader,
    CancellationToken token)
    {
        await UniTask.SwitchToThreadPool();
        await action(reader, classDataHeader).AttachExternalCancellation(token);
        await UniTask.SwitchToMainThread();
    }

}

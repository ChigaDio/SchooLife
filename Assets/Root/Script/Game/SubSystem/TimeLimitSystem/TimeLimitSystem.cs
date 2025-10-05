using Cysharp.Threading.Tasks;
using GameCore.Enums;
using GameCore.SaveSystem;
using GameCore.Tables;
using GameCore.Tables.ID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class TimeLimitSystem
{
    private static readonly float MAXTIMELIMIT = 5.0f;

    // ジェネリック型を使った統合メソッド
    public async UniTask UpdateCalculationCommand<TKey, TValue>(
        Action<float> action,
        Action<TKey> onComplete,
        PlayerData playerData,
        IDictionary<PersonalityTableID, Dictionary<TKey, TValue>> table,
        TKey defaultResult,
        Func<TValue, float> probabilitySelector,
        IEnumerable<TKey> excludedKeys = null,
        CancellationToken cancellationToken = default)
        where TValue : BaseClassDataMatrixRow
    {
        // シーン破棄用のキャンセルトークンを取得し、呼び出し元のトークンとリンク
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            MainSceneCore.Instance.GetCancellationTokenOnDestroy()
        );

        // テーブルにデータがない場合の処理
        if (!table.ContainsKey(playerData.personalityTableID))
        {
            action?.Invoke(0.0f);
            onComplete?.Invoke(defaultResult);
            return;
        }

        var data = table[playerData.personalityTableID];
        var result = SelectResult(data, probabilitySelector, excludedKeys, cts.Token);
        await UpdateCalculation(action, result, onComplete, cts.Token);
    }

    // 確率計算を共通化
    private TKey SelectResult<TKey, TValue>(
        Dictionary<TKey, TValue> data,
        Func<TValue, float> probabilitySelector,
        IEnumerable<TKey> excludedKeys,
        CancellationToken cancellationToken)
        where TValue : BaseClassDataMatrixRow
    {
        float totalProb = 0.0f;
        float findRange = 0.0f;
        TKey result = default;

        // 除外キーを考慮して確率の合計を計算
        foreach (var row in data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (excludedKeys != null && excludedKeys.Contains(row.Key)) continue;
            totalProb += probabilitySelector(row.Value);
        }

        findRange = UnityEngine.Random.Range(0.0f, totalProb);
        float cumulative = 0.0f;
        foreach (var row in data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (excludedKeys != null && excludedKeys.Contains(row.Key)) continue;
            cumulative += probabilitySelector(row.Value);
            if (cumulative >= findRange)
            {
                result = row.Key;
                break;
            }
        }

        return result;
    }

    // タイマー処理を共通化
    private async UniTask UpdateCalculation<TKey>(
        Action<float> action,
        TKey result,
        Action<TKey> onComplete,
        CancellationToken cancellationToken)
    {
        float time = 0.0f;
        action?.Invoke(1.0f);
        try
        {
            while (time <= MAXTIMELIMIT)
            {
                if(OptionCanvas.Instance.GetActive())
                {
                    await UniTask.DelayFrame(30, cancellationToken: cancellationToken);
                }
                cancellationToken.ThrowIfCancellationRequested();
                float t = time / MAXTIMELIMIT;
                float lerp = Mathf.Lerp(1.0f, 0.0f, t);
                action?.Invoke(lerp);
                time += Time.deltaTime;
                await UniTask.DelayFrame(2, cancellationToken: cancellationToken);
            }

            action?.Invoke(0.0f);
            onComplete?.Invoke(result);
        }
        catch (OperationCanceledException)
        {
            action?.Invoke(0.0f);
            throw;
        }
    }

    // StudyCommand用のラッパーメソッド
    public async UniTask UpdateCalculationStudyCommand(
        Action<float> action,
        Action<CharacterStatID> onComplete,
        PlayerData playerData,
        CancellationToken cancellationToken = default)
    {
        await UpdateCalculationCommand(
            action,
            onComplete,
            playerData,
            PersonalityStatWeightsMatrixTable.Table,
            CharacterStatID.Study,
            value => value.Actionprobability,
            null,
            cancellationToken);
    }

    // ActionCommand用のラッパーメソッド
    public async UniTask UpdateCalculationActionCommand(
        Action<float> action,
        Action<ActionExecuteCommandTableID> onComplete,
        PlayerData playerData,
        CancellationToken cancellationToken = default)
    {
        var excludedKeys = new List<ActionExecuteCommandTableID>
        {
            ActionExecuteCommandTableID.Option,
            ActionExecuteCommandTableID.Finish
        };
        await UpdateCalculationCommand(
            action,
            onComplete,
            playerData,
            PersonalityActionExecuteWeightsMatrixTable.Table,
            ActionExecuteCommandTableID.Study,
            value => value.Actionprobability,
            excludedKeys,
            cancellationToken);
    }
}


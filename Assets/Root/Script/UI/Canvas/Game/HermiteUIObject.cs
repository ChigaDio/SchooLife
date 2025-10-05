using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

[Serializable]
public class HermiteUIObject
{
    private GameObject gameObject;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMesh;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 controlDir;
    private float strength;
    private float duration;
    public bool IsActive => gameObject != null && gameObject.activeSelf;
    private int num = 0;
    public int GetNum {  get { return num; } }
    public HermiteUIObject(GameObject obj, Vector3 start, Vector3 target,int valueNumt)
    {
        gameObject = obj;
        rectTransform = obj.GetComponent<RectTransform>();
        textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();

        num = valueNumt;
        startPos = start;
        targetPos = target;
        rectTransform.anchoredPosition3D = start;
        if (textMesh != null)
            textMesh.text = num.ToString();

        // •ûŒü‚Æ§Œä“_İ’è
        var dir = (target - start).normalized;

        float angle = UnityEngine.Random.Range(0, 2) == 0 ?
            UnityEngine.Random.Range(45f, 180f) :
            UnityEngine.Random.Range(-45f, -180f);

        strength = UnityEngine.Random.Range(100f, 500f);

        controlDir = Quaternion.Euler(0, 0, angle) * dir * strength;
        duration = UnityEngine.Random.Range(0.75f, 1.25f);
    }

    /// <summary>
    /// Hermite•âŠÔ‚ÅˆÚ“®
    /// ƒS[ƒ‹“’BŒã‚É”ñ“¯Šú‚Å”jŠü
    /// </summary>
    public async UniTask MoveAndDestroyAsync(float destroyDelay = 0f, CancellationToken cancellationToken = default)
    {
        float t = 0f;
        while (t < 1f)
        {
            cancellationToken.ThrowIfCancellationRequested();
            t += Time.deltaTime / duration;
            Vector3 p = CalculateHermitePoint(t);
            rectTransform.position = p;
            await UniTask.Yield(cancellationToken: cancellationToken);
        }

        cancellationToken.ThrowIfCancellationRequested();
        rectTransform.position = targetPos;

        // ”CˆÓ‚Ì’x‰„Œã‚É”ñ“¯Šú”jŠü
        if (destroyDelay > 0f)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(destroyDelay), cancellationToken: cancellationToken);
        }

        DestroySelf();
    }

    /// <summary>
    /// ©•ª©g‚ğ”jŠü
    /// </summary>
    private void DestroySelf()
    {
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
            gameObject = null;
            rectTransform = null;
            textMesh = null;
        }
    }

    private Vector3 CalculateHermitePoint(float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        Vector3 h00 = (2 * t3 - 3 * t2 + 1) * startPos;
        Vector3 h10 = (t3 - 2 * t2 + t) * controlDir;
        Vector3 h01 = (-2 * t3 + 3 * t2) * targetPos;
        Vector3 h11 = (t3 - t2) * (-controlDir);

        return h00 + h10 + h01 + h11;
    }
}
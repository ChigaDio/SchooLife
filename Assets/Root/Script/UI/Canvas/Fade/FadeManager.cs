using Cysharp.Threading.Tasks;
using GameCore;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FadeManager : BaseSingleton<FadeManager>
{
    [SerializeField] private CanvasGroup fadeCanvasGroup; // フェード用のCanvasGroup
    private bool isFading = false; // フェード中フラグ
    public bool IsFading => isFading; // フェード中かどうかを取得
    public bool IsFadedIn => fadeCanvasGroup != null && Mathf.Approximately(fadeCanvasGroup.alpha, 1f); // フェードイン完了状態
    public bool IsFadedOut => fadeCanvasGroup != null && Mathf.Approximately(fadeCanvasGroup.alpha, 0f); // フェードアウト完了状態

    public override void AwakeSingleton()
    {
        base.AwakeSingleton();

        // CanvasGroup が設定されていない場合、自動生成
        if (fadeCanvasGroup == null)
        {
            CreateFadeCanvas();
        }
        DontDestroyOnLoad(gameObject);
    }

    // フェード用のCanvasとCanvasGroupを自動生成
    private void CreateFadeCanvas()
    {
        GameObject canvasObj = new GameObject("FadeCanvas");
        canvasObj.transform.SetParent(transform);
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // 最前面に表示

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        fadeCanvasGroup = canvasObj.AddComponent<CanvasGroup>();
        fadeCanvasGroup.alpha = 0f; // 初期は透明
        fadeCanvasGroup.blocksRaycasts = true; // フェード中は入力ブロック

        // 黒い背景画像を追加
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);
        UnityEngine.UI.Image image = imageObj.AddComponent<UnityEngine.UI.Image>();
        image.color = Color.black; // 黒でフェード
        image.rectTransform.anchorMin = Vector2.zero;
        image.rectTransform.anchorMax = Vector2.one;
        image.rectTransform.offsetMin = Vector2.zero;
        image.rectTransform.offsetMax = Vector2.zero;
    }

    // フェードイン
    public async UniTask FadeIn(float duration, bool force = false, Action onComplete = null)
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("FadeCanvasGroup is not assigned!");
            return;
        }

        if (isFading)
        {
            Debug.LogWarning("Fade is already in progress!");
            return;
        }

        isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float startAlpha = force ? 0f : fadeCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            await UniTask.Yield();
        }

        fadeCanvasGroup.alpha = 0f; // 最終的に確実に1.0
        isFading = false;
        fadeCanvasGroup.blocksRaycasts = false; // フェードイン完了後もブロック
        onComplete?.Invoke();
    }

    // フェードアウト
    public async UniTask FadeOut(float duration, bool force = false, Action onComplete = null)
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("FadeCanvasGroup is not assigned!");
            return;
        }

        if (isFading)
        {
            Debug.LogWarning("Fade is already in progress!");
            return;
        }

        isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float startAlpha = force ? 1f : fadeCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsed / duration);
            await UniTask.Yield();
        }

        fadeCanvasGroup.alpha = 1f; // 最終的に確実に0.0
        isFading = false;
        fadeCanvasGroup.blocksRaycasts = true; // フェードアウト完了後は入力許可
        onComplete?.Invoke();
    }

    // 現在のアルファ値を即座に設定（フェードなし）
    public void SetAlphaImmediate(float alpha)
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = Mathf.Clamp01(alpha);
            fadeCanvasGroup.blocksRaycasts = alpha > 0f;
        }
    }
}
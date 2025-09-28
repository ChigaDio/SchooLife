using Cysharp.Threading.Tasks;
using GameCore;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FadeManager : BaseSingleton<FadeManager>
{
    [SerializeField] private CanvasGroup fadeCanvasGroup; // �t�F�[�h�p��CanvasGroup
    private bool isFading = false; // �t�F�[�h���t���O
    public bool IsFading => isFading; // �t�F�[�h�����ǂ������擾
    public bool IsFadedIn => fadeCanvasGroup != null && Mathf.Approximately(fadeCanvasGroup.alpha, 1f); // �t�F�[�h�C���������
    public bool IsFadedOut => fadeCanvasGroup != null && Mathf.Approximately(fadeCanvasGroup.alpha, 0f); // �t�F�[�h�A�E�g�������

    public override void AwakeSingleton()
    {
        base.AwakeSingleton();

        // CanvasGroup ���ݒ肳��Ă��Ȃ��ꍇ�A��������
        if (fadeCanvasGroup == null)
        {
            CreateFadeCanvas();
        }
        DontDestroyOnLoad(gameObject);
    }

    // �t�F�[�h�p��Canvas��CanvasGroup����������
    private void CreateFadeCanvas()
    {
        GameObject canvasObj = new GameObject("FadeCanvas");
        canvasObj.transform.SetParent(transform);
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // �őO�ʂɕ\��

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        fadeCanvasGroup = canvasObj.AddComponent<CanvasGroup>();
        fadeCanvasGroup.alpha = 0f; // �����͓���
        fadeCanvasGroup.blocksRaycasts = true; // �t�F�[�h���͓��̓u���b�N

        // �����w�i�摜��ǉ�
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);
        UnityEngine.UI.Image image = imageObj.AddComponent<UnityEngine.UI.Image>();
        image.color = Color.black; // ���Ńt�F�[�h
        image.rectTransform.anchorMin = Vector2.zero;
        image.rectTransform.anchorMax = Vector2.one;
        image.rectTransform.offsetMin = Vector2.zero;
        image.rectTransform.offsetMax = Vector2.zero;
    }

    // �t�F�[�h�C��
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

        fadeCanvasGroup.alpha = 0f; // �ŏI�I�Ɋm����1.0
        isFading = false;
        fadeCanvasGroup.blocksRaycasts = false; // �t�F�[�h�C����������u���b�N
        onComplete?.Invoke();
    }

    // �t�F�[�h�A�E�g
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

        fadeCanvasGroup.alpha = 1f; // �ŏI�I�Ɋm����0.0
        isFading = false;
        fadeCanvasGroup.blocksRaycasts = true; // �t�F�[�h�A�E�g������͓��͋���
        onComplete?.Invoke();
    }

    // ���݂̃A���t�@�l�𑦍��ɐݒ�i�t�F�[�h�Ȃ��j
    public void SetAlphaImmediate(float alpha)
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = Mathf.Clamp01(alpha);
            fadeCanvasGroup.blocksRaycasts = alpha > 0f;
        }
    }
}
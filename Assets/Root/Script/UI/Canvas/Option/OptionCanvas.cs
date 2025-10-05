using Cysharp.Threading.Tasks;
using GameCore;
using GameCore.SaveSystem;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static TitleCanvas;

/// <summary>
/// �I�v�V�����ݒ�
/// </summary>
public class OptionCanvas : BaseSingleton<OptionCanvas>
{
    [SerializeField] private Image fadeImg = null;

    [SerializeField] private RectTransform parent = null;
    public void OnParentActive() { if (parent == null) return; parent.gameObject.SetActive(true); }
    public void OffParentActive() { if (parent == null) return;  parent.gameObject.SetActive(false); }
    public bool GetActive()
    {
        if (parent == null) return false;
        return parent.gameObject.activeSelf;
    }
    public enum OptionSlider
    {
        BGM,
        SE,
        MAX
    }
    /// <summary>
    /// �X���C�_�[
    /// </summary>
    [SerializeField]
    private Slider[] sliderArray = new Slider[(int)OptionSlider.MAX];

    public float GetSliderValue(OptionSlider slider)
    {
        return sliderArray[(int)slider].value;
    }
    public void SetSliderValue(OptionSlider slider,float value)
    {
        sliderArray[(int)slider].value = value;
    }

    /// <summary>
    /// �Z�[�u�{�^��
    /// </summary>
    [SerializeField]
    private Button saveButton = null;
    private UnityAction saveButtonAction = null;

    /// <summary>
    /// �L�����Z���g�[�N��
    /// </summary>
    private CancellationToken destroyToken;

    public override void AwakeSingleton()
    {
        base.AwakeSingleton();
        destroyToken = this.GetCancellationTokenOnDestroy();
        DontDestroyOnLoad(this);

        SetSliderValue(OptionSlider.BGM, SaveManagerCore.instance.SystemSettings.bgmVolume);
        SetSliderValue(OptionSlider.SE, SaveManagerCore.instance.SystemSettings.seVolume);
    }

    public void Update()
    {
        if (SaveManagerCore.instance.IsSaveLoadActionNow()) return;
        SaveManagerCore.instance.SystemSettings.bgmVolume = GetSliderValue(OptionSlider.BGM);
        SaveManagerCore.instance.SystemSettings.seVolume = GetSliderValue(OptionSlider.SE);
    }



    /// <summary>
    /// �t�F�[�h�A�E�g
    /// </summary>
    /// <param name="fadeTime"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public async UniTask FadeOutAsync(float fadeTime, Action action = null)
    {
        if (!fadeImg)
        {
            action?.Invoke();
            return;
        }

        float startVolume = fadeImg.color.a;
        var color = fadeImg.color;
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            var alpha  = Mathf.Lerp(startVolume, 0f, timer / fadeTime);
            color.a = alpha;
            fadeImg.color = color;
            if (fadeTime >= timer)
            {
                alpha = 0.0f;
                fadeImg.color = color;
                break;
            }
            await UniTask.Yield(cancellationToken: destroyToken);
        }
        action?.Invoke();
    }

    /// <summary>
    /// �t�F�[�h�C��
    /// </summary>
    /// <param name="targetVolume"></param>
    /// <param name="fadeTime"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public async UniTask FadeInAsync(float targetVolume, float fadeTime, Action action = null)
    {
        float timer = 0f;

        float startVolume = fadeImg.color.a;
        var color = fadeImg.color;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            var alpha = Mathf.Lerp(startVolume, fadeTime, timer / fadeTime);
            color.a = alpha;
            fadeImg.color = color;
            if (fadeTime >= timer)
            {
                alpha = targetVolume;
                fadeImg.color = color;
                break;
            }
            await UniTask.Yield(cancellationToken: destroyToken);
        }
        action?.Invoke();
    }

    public void AddListenerButton(UnityAction action)
    {

        if (saveButton == null) return;
        // Remove previous listener
        if (saveButtonAction != null)
        {
            saveButton.onClick.RemoveListener(saveButtonAction);
            saveButtonAction = null;
        }

        // Add new listener
        saveButtonAction = action;
        saveButton.onClick.AddListener(saveButtonAction);

    }


    private void OnDestroy()
    {
        if (saveButton == null) return;
        if (saveButtonAction == null) return;
        saveButton.onClick.RemoveListener(saveButtonAction);
        saveButtonAction = null;
    }
}

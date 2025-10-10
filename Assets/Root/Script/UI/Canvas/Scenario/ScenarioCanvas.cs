using Cysharp.Threading.Tasks;
using GameCore;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static ScenarioCanvas.TalkSelect;

/// <summary>
/// シナリオUIキャンバス
/// </summary>
public class ScenarioCanvas : BaseSingleton<ScenarioCanvas>
{
    [Serializable]
    public class ButtonSelect
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TMPro.TextMeshProUGUI buttonText;

        public void SetText(string text)
        {
            buttonText.text = text;
        }

        private UnityEngine.Events.UnityAction action;

        public void RemoveAction()
        {
            if (button == null)
            {
                return;
            }
            if(action == null)
            {
                return;
            }

            button.onClick.RemoveListener(action);
        }

        public void AddAction(UnityEngine.Events.UnityAction valueAction)
        {
            if (button == null)
            {
                action.Invoke();
            }

            RemoveAction();

            button.onClick.AddListener(valueAction);

            action = valueAction;
        }
    }
    [Serializable]

    public class TalkSelect
    {
        /// <summary>
        /// セレクトID
        /// </summary>
        public enum ButtonSelectID
        {
            SelectTop,
            SelectBottom,
            Max
        }

        /// <summary>
        /// ボタン配列
        /// </summary>
        [SerializeField]
        private ButtonSelect[] buttonSelectArray = new ButtonSelect[(int)ButtonSelectID.Max];

        /// <summary>
        /// アニメーション
        /// </summary>
        public GameAnimatorController<ScenarioSelectTalk, ScenarioSelectTalkEnum> animator = new GameAnimatorController<ScenarioSelectTalk, ScenarioSelectTalkEnum>();

        public void SetUp(GameObject gameObject)
        {
            animator.SetUp(gameObject);
        }

        public void SetText(string select01,string select02)
        {
            buttonSelectArray[(int)ButtonSelectID.SelectTop].SetText(select01);
            buttonSelectArray[(int)ButtonSelectID.SelectBottom].SetText(select02);
        }

        public void AddButtonEvent(ButtonSelectID id,UnityEngine.Events.UnityAction action) 
        {
            if(id >= ButtonSelectID.Max || id < ButtonSelectID.SelectTop)
            {
                action.Invoke();
            }

            var result = buttonSelectArray[(int)id];
            if(result == null)
            {
                action.Invoke();
                return;
            }

            result.AddAction(action);

        }

        public void RemoveButtonEvent(ButtonSelectID id)
        {
            if (id >= ButtonSelectID.Max || id < ButtonSelectID.SelectTop)
            {
                return;
            }

            var result = buttonSelectArray[(int)id];
            if (result == null)
            {
                return;
            }

            result.RemoveAction();

        }

        public void AllRemoveButtonEvent()
        {
            foreach(var button in buttonSelectArray)
            {
                button.RemoveAction();
            }
        }

    }
    /// <summary>
    /// 会話フレーム
    /// </summary>
    [Serializable]
    public class TalkFrame
    {

        [SerializeField]
        private CanvasGroup canvasGroup;
        /// <summary>
        /// 名前
        /// </summary>
        [SerializeField]
        private TMPro.TextMeshProUGUI nameTag;

        /// <summary>
        /// 会話
        /// </summary>
        [SerializeField]
        private TMPro.TextMeshProUGUI talkText;

        /// <summary>
        /// オブジェクトのデストロイ判定Token
        /// </summary>
        private CancellationToken token;

        private CancellationToken clickToken;

        public void SetUp(GameObject gameObject)
        {
            token = gameObject.GetCancellationTokenOnDestroy();
        }

        public async UniTask UpdateTalkText(string text, string name, Action<CancellationTokenSource> action, Action onComp = null, float speedMul = 1.0f)
        {
            talkText.text = "";
            nameTag.text = name;

            // キャンセルトークンソースを作成（クリック用）
            using var cts = new CancellationTokenSource();
            clickToken = cts.Token;

            // オブジェクト破棄トークンとクリックトークンを結合
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, clickToken);


            // アクションを実行してキャンセルトークンを渡す
            action?.Invoke(linkedCts);
            try
            {
                // 1文字ずつ表示
                for (int i = 0; i < text.Length; i++)
                {
                    // キャンセル（オブジェクト破棄またはクリック）されたら全テキスト表示
                    if (linkedCts.Token.IsCancellationRequested)
                    {
                        talkText.text = text;
                        return;
                    }

                    // 現在の文字を追加
                    talkText.text = text.Substring(0, i + 1);

                    // 表示速度に基づいて待機（speedMulで調整）
                    float time = 0.05f / speedMul; // 1文字あたり0.05秒を基準

                    await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: linkedCts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                // オブジェクト破棄またはクリックによるキャンセル時に全テキスト表示
                talkText.text = text;
                onComp?.Invoke();
                return;
            }

            linkedCts.Cancel();
            talkText.text = text;
            onComp?.Invoke();
        }

        public void FadeAlpha(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            canvasGroup.alpha = alpha;
        }
    }

    /// <summary>
    /// 選択会話
    /// </summary>
    [SerializeField]
    private TalkSelect talkSelect = new TalkSelect();
    public GameAnimatorController<ScenarioSelectTalk, ScenarioSelectTalkEnum> AnimatorScenarioSelect { get { return talkSelect.animator; } }

    /// <summary>
    /// 会話フレーム
    /// </summary>
    [SerializeField]
    private TalkFrame talkFrame = new TalkFrame();

    public void AddButtonEvent(ButtonSelectID id, UnityEngine.Events.UnityAction action)
    {
        talkSelect.AddButtonEvent(id, action);

    }

    public void RemoveButtonEvent(ButtonSelectID id)
    {

        talkSelect.RemoveButtonEvent(id);
    }

    public void AllRemoveButtonEvent()
    { 
        talkSelect.AllRemoveButtonEvent();
    }

    public void FadeTalkFrameAlpha(float alpha)
    {
        talkFrame.FadeAlpha(alpha);
    }

    public void UpdateTalkText(string text, string name, Action<CancellationTokenSource> action, Action onComp = null, float speedMul = 1.0f)
    {
        talkFrame.UpdateTalkText(text, name, action, onComp, speedMul).Forget();
    }


    public override void AwakeSingleton()
    {
        base.AwakeSingleton();
        talkSelect.SetUp(gameObject);
        talkFrame.SetUp(gameObject);
    }

    public void SetText(string select01, string select02)
    {
        talkSelect.SetText(select01, select02);
    }
}

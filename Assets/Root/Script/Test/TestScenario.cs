using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScenario : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async UniTask Start()
    {
        ScenarioCanvas.Instance.SetText("", "");
        ScenarioCanvas.Instance.FadeTalkFrameAlpha(1.0f);
        ScenarioCanvas.Instance.UpdateTalkText("これはまだテストです。", "テスト太郎",
            action: (token) =>
            {
                // UniTaskでクリックを監視（非同期）
                UniTask.RunOnThreadPool(async () => {
                    while (!token.Token.IsCancellationRequested)
                    {
                        if (Mouse.current.leftButton.wasPressedThisFrame)  // 新しいInput SystemのMouse
                        {
                            token.Cancel();
                            break;
                        }
                        await UniTask.Delay(16, cancellationToken: token.Token);  // 約60FPSでポーリング
                    }
                }).Forget();  // Forgetでバックグラウンド実行
            },
            onComp: () =>
            {
                ScenarioCanvas.Instance.SetText("テスト1", "テスト2");
                ScenarioCanvas.Instance.AnimatorScenarioSelect.Play(ScenarioSelectTalkEnum.Base_Layer_Start, onComplete: () =>
                {
                    ScenarioCanvas.Instance.AddButtonEvent(ScenarioCanvas.TalkSelect.ButtonSelectID.SelectTop, () =>
                    {
                        ScenarioCanvas.Instance.AnimatorScenarioSelect.Play(ScenarioSelectTalkEnum.Base_Layer_Select01, transitionDuration: 0.0f);
                    });
                    ScenarioCanvas.Instance.AddButtonEvent(ScenarioCanvas.TalkSelect.ButtonSelectID.SelectBottom, () =>
                    {
                        ScenarioCanvas.Instance.AnimatorScenarioSelect.Play(ScenarioSelectTalkEnum.Base_Layer_Select02, transitionDuration: 0.0f);
                    });
                });
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

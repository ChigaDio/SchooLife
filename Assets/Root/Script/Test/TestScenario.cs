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
        ScenarioCanvas.Instance.UpdateTalkText("����͂܂��e�X�g�ł��B", "�e�X�g���Y",
            action: (token) =>
            {
                // UniTask�ŃN���b�N���Ď��i�񓯊��j
                UniTask.RunOnThreadPool(async () => {
                    while (!token.Token.IsCancellationRequested)
                    {
                        if (Mouse.current.leftButton.wasPressedThisFrame)  // �V����Input System��Mouse
                        {
                            token.Cancel();
                            break;
                        }
                        await UniTask.Delay(16, cancellationToken: token.Token);  // ��60FPS�Ń|�[�����O
                    }
                }).Forget();  // Forget�Ńo�b�N�O���E���h���s
            },
            onComp: () =>
            {
                ScenarioCanvas.Instance.SetText("�e�X�g1", "�e�X�g2");
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

using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameCore.Scenario {
    public class TalkTextRoleAction : BaseScenarioRoleAction<TalkTextRoleData> {

        public bool auto = false;
        public TalkTextRoleAction(TalkTextRoleData roleData) : base(roleData) {
        }

        public override void OnInitialize(ScenarioExecuteData executeData) {
            // Custom initialization logic
            base.OnInitialize(executeData);
            IsStartUp = true;
        }

        public override void OnOneExecute(ScenarioExecuteData executeData)
        {
            ScenarioCanvas.Instance.UpdateTalkText(RoleData.text, RoleData.name,
                            action: (token) =>
                            {
                                // UniTaskでクリックを監視（非同期）
                                UniTask.RunOnThreadPool(async () => {
                                    while (!token.Token.IsCancellationRequested)
                                    {
                                        if (Mouse.current.leftButton.wasPressedThisFrame)  // 新しいInput SystemのMouse
                                        {
                                            token.Cancel();
                                            IsCompleted = true;
                                            break;
                                        }
                                        await UniTask.Delay(16, cancellationToken: token.Token);  // 約60FPSでポーリング
                                    }
                                }).Forget();  // Forgetでバックグラウンド実行
                            },
                            onComp: async () =>
                            {
                                await UniTask.Delay(3000);
                                IsCompleted = true;
                            });
            IsOneExecute = true;
        }

        public override void OnExecute(ScenarioExecuteData executeData) {

        }

        public override void OnFinalize(ScenarioExecuteData executeData) {
            // Custom cleanup logic
            IsRelease = true;
        }
    }
}

using UnityEngine;

using GameCore.States.Branch;
using Cysharp.Threading.Tasks;
using GameCore.Sound;
using AddressableSystem;
namespace GameCore.States
{
    public class TitleSceneFadeOutState : BaseTitleSceneFadeOutState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            FadeManager.Instance.FadeOut(0.5f, onComplete: () =>
            {
                SoundCore.Instance.FadeOutAsync(0.5f, action: () =>
                {
                    IsActiveOff();
                }).Forget();
            }).Forget();

        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}

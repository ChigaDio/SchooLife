using UnityEngine;

using GameCore.States.Branch;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class TitleSceneFadeInState : BaseTitleSceneFadeInState
    {
        public override void Enter(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) 
        {
            FadeManager.Instance.FadeIn(1.0f, onComplete: () =>
            {
                IsActiveOff();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleSceneStateManagerData state_manager_data) { }
    }
}

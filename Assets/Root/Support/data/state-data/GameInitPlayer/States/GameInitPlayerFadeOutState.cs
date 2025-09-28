using UnityEngine;

using GameCore.States.Branch;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class GameInitPlayerFadeOutState : BaseGameInitPlayerFadeOutState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) 
        {
            FadeManager.Instance.FadeOut(0.5f, onComplete: () =>
            {
                IsActiveOff();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}

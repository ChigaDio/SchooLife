using UnityEngine;

using GameCore.States.Branch;
using Cysharp.Threading.Tasks;
namespace GameCore.States
{
    public class GamePlayFadeInState : BaseGamePlayFadeInState
    {
        public override void Enter(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) 
        {
            FadeManager.Instance.FadeIn(0.5f, onComplete: () =>
            {
                IsActiveOff();
            }).Forget();
        }
        public override void Update(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
    }
}

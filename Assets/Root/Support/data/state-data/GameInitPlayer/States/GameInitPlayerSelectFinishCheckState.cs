using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class GameInitPlayerSelectFinishCheckState : BaseGameInitPlayerSelectFinishCheckState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) 
        {
            PlayerInitCanvas.Instance.FinishButtonSelectItem(() =>
            {
                IsActiveOff();
            });
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}

using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class GameInitPlayerInputFinishAnimationState : BaseGameInitPlayerInputFinishAnimationState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data)
        {
            IsActiveOff();
            PlayerInitCanvas.Instance.SetActive(false, PlayerInitCanvas.ParentField.Input);
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}

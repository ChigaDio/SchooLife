using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class GameInitPlayerSelectItemFinishAnimationState : BaseGameInitPlayerSelectItemFinishAnimationState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data)
        {
            IsActiveOff();
            PlayerInitCanvas.Instance.SetActive(false, PlayerInitCanvas.ParentField.Select);
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}

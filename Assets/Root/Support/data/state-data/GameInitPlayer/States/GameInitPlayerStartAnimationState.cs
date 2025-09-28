using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class GameInitPlayerStartAnimationState : BaseGameInitPlayerStartAnimationState
    {
        public override void Enter(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data)
        {
            PlayerInitCanvas.Instance.SetActive(true, PlayerInitCanvas.ParentField.Input);
            IsActiveOff();
        }
        public override void Update(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameInitPlayerStateManagerData state_manager_data) { }
    }
}

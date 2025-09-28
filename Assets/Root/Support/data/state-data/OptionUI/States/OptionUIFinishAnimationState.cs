using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class OptionUIFinishAnimationState : BaseOptionUIFinishAnimationState
    {
        public override void Enter(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) 
        {
            OptionCanvas.Instance.OffParentActive();
            IsActiveOff();
        }
        public override void Update(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.OptionUIStateManagerData state_manager_data) { }
    }
}

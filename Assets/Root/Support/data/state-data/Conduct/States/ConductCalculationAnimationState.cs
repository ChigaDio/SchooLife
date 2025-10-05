using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class ConductCalculationAnimationState : BaseConductCalculationAnimationState
    {
        public override void Enter(GameCore.States.Managers.ConductStateManagerData state_manager_data)
        {
            IsActiveOff();
        }
        public override void Update(GameCore.States.Managers.ConductStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.ConductStateManagerData state_manager_data) { }
    }
}

using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class MainLoopExitState : BaseMainLoopExitState
    {
        public override void Enter(GameCore.States.Managers.MainLoopStateManagerData state_manager_data) { IsActiveOff(); }
        public override void Update(GameCore.States.Managers.MainLoopStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.MainLoopStateManagerData state_manager_data) { }
    }
}

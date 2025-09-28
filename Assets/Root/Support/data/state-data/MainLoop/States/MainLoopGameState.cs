using UnityEngine;

using GameCore.States.Branch;
using GameCore.States.Control;
namespace GameCore.States
{
    public class MainLoopGameState : BaseMainLoopGameState
    {
        GameSceneStateControl ctl = new GameSceneStateControl();
        public override void Enter(GameCore.States.Managers.MainLoopStateManagerData state_manager_data)
        {
            ctl.StartState();
        }
        public override void Update(GameCore.States.Managers.MainLoopStateManagerData state_manager_data) 
        {
            ctl.UpdateState();
            if(ctl.IsFinish)
            {
                IsActiveOff();
            }
        }
        public override void Exit(GameCore.States.Managers.MainLoopStateManagerData state_manager_data) { }
    
        public override GameCore.States.ID.MainLoopStateID BranchNextState(GameCore.States.Managers.MainLoopStateManagerData state_manager_data)
        {
            var branch = new MainLoopGameStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}
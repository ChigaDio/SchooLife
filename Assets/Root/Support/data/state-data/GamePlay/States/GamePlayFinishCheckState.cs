using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class GamePlayFinishCheckState : BaseGamePlayFinishCheckState
    {
        public override void Enter(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
        public override void Update(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GamePlayStateManagerData state_manager_data) { }
        public override GameCore.States.ID.GamePlayStateID BranchNextState(GameCore.States.Managers.GamePlayStateManagerData state_manager_data)
        {
            var branch = new GamePlayFinishCheckStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}

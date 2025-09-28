using UnityEngine;

using GameCore.States.Branch;
namespace GameCore.States
{
    public class GameSceneSwitchGameLoadState : BaseGameSceneSwitchGameLoadState
    {
        public override void Enter(GameCore.States.Managers.GameSceneStateManagerData state_manager_data)
        {
            IsActiveOff();
        }
        public override void Update(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.GameSceneStateManagerData state_manager_data) { }
        public override GameCore.States.ID.GameSceneStateID BranchNextState(GameCore.States.Managers.GameSceneStateManagerData state_manager_data)
        {
            var branch = new GameSceneSwitchGameLoadStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}

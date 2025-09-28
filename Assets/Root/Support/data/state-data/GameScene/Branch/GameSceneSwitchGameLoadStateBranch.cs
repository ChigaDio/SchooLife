using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GameSceneSwitchGameLoadStateBranch : BaseGameSceneStateBranch<GameSceneSwitchGameLoadState, BaseGameSceneSwitchGameLoadDetailStateBranch>
    {
        public override GameSceneStateID ConditionsBranch(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GameSceneStateID.None;
        }

        public override BaseGameSceneSwitchGameLoadDetailStateBranch Factory(GameSceneStateID id)
        {
            switch (id)
            {
                case GameSceneStateID.SwitchGameLoad02:
                    return new GameSceneSwitchGameLoad02DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GamePlayFinishCheckStateBranch : BaseGamePlayStateBranch<GamePlayFinishCheckState, BaseGamePlayFinishCheckDetailStateBranch>
    {
        public override GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GamePlayStateID.None;
        }

        public override BaseGamePlayFinishCheckDetailStateBranch Factory(GamePlayStateID id)
        {
            switch (id)
            {
                case GamePlayStateID.FinishCheck10:
                    return new GamePlayFinishCheck10DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GamePlayEventCheckStateBranch : BaseGamePlayStateBranch<GamePlayEventCheckState, BaseGamePlayEventCheckDetailStateBranch>
    {
        public override GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlayEventCheckState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GamePlayStateID.None;
        }

        public override BaseGamePlayEventCheckDetailStateBranch Factory(GamePlayStateID id)
        {
            switch (id)
            {
                case GamePlayStateID.EventCheck06:
                    return new GamePlayEventCheck06DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

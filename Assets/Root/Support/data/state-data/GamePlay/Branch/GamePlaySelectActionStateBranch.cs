using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GamePlaySelectActionStateBranch : BaseGamePlayStateBranch<GamePlaySelectActionState, BaseGamePlaySelectActionDetailStateBranch>
    {
        public override GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlaySelectActionState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GamePlayStateID.None;
        }

        public override BaseGamePlaySelectActionDetailStateBranch Factory(GamePlayStateID id)
        {
            switch (id)
            {
                case GamePlayStateID.SelectAction02:
                    return new GamePlaySelectAction02DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

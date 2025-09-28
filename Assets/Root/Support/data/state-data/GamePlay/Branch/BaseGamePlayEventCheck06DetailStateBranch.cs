using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlayEventCheck06DetailStateBranch : BaseGamePlayEventCheckDetailStateBranch
    {
        public override GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlayEventCheckState state)
        {
            if (GamePlayEventCheck_to_Event08(manager_data, state))
                return GamePlayStateID.Event08;
            if (GamePlayEventCheck_to_Save09(manager_data, state))
                return GamePlayStateID.Save09;
            return GamePlayStateID.None;
        }

        public override abstract bool GamePlayEventCheck_to_Event08(GamePlayStateManagerData manager_data, GamePlayEventCheckState state);
        public override abstract bool GamePlayEventCheck_to_Save09(GamePlayStateManagerData manager_data, GamePlayEventCheckState state);
    }
}

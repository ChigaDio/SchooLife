using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlayFinishCheck10DetailStateBranch : BaseGamePlayFinishCheckDetailStateBranch
    {
        public override GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state)
        {
            if (GamePlayFinishCheck_to_FinishExit11(manager_data, state))
                return GamePlayStateID.FinishExit11;
            if (GamePlayFinishCheck_to_FadeIn01(manager_data, state))
                return GamePlayStateID.FadeIn01;
            return GamePlayStateID.None;
        }

        public override abstract bool GamePlayFinishCheck_to_FinishExit11(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state);
        public override abstract bool GamePlayFinishCheck_to_FadeIn01(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state);
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlayFinishCheckDetailStateBranch : BaseGamePlayDetailStateBranch<GamePlayFinishCheckState>
    {
        public override abstract GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state);
        public abstract bool GamePlayFinishCheck_to_FinishExit11(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state);
        public abstract bool GamePlayFinishCheck_to_FadeIn01(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state);
    }
}

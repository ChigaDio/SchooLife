using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGamePlayDetailStateBranch<TState> : BaseDetailStateBranch<GamePlayStateID, GamePlayStateManagerData, TState>
        where TState : GameCore.States.BaseGamePlayState
    {
        public override abstract GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, TState state);
    }
}

using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseGamePlayStateBranch<TState, TDetailState> : BaseStateBranch<GamePlayStateID, GamePlayStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseGamePlayState
        where TDetailState : BaseGamePlayDetailStateBranch<TState>
    {
        public override abstract GamePlayStateID ConditionsBranch(GamePlayStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(GamePlayStateID id);
    }
}

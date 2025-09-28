using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseGameSceneStateBranch<TState, TDetailState> : BaseStateBranch<GameSceneStateID, GameSceneStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseGameSceneState
        where TDetailState : BaseGameSceneDetailStateBranch<TState>
    {
        public override abstract GameSceneStateID ConditionsBranch(GameSceneStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(GameSceneStateID id);
    }
}

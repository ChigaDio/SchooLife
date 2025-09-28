using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGameSceneDetailStateBranch<TState> : BaseDetailStateBranch<GameSceneStateID, GameSceneStateManagerData, TState>
        where TState : GameCore.States.BaseGameSceneState
    {
        public override abstract GameSceneStateID ConditionsBranch(GameSceneStateManagerData manager_data, TState state);
    }
}

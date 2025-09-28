using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseGameInitPlayerStateBranch<TState, TDetailState> : BaseStateBranch<GameInitPlayerStateID, GameInitPlayerStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseGameInitPlayerState
        where TDetailState : BaseGameInitPlayerDetailStateBranch<TState>
    {
        public override abstract GameInitPlayerStateID ConditionsBranch(GameInitPlayerStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(GameInitPlayerStateID id);
    }
}

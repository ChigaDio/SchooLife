using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGameInitPlayerDetailStateBranch<TState> : BaseDetailStateBranch<GameInitPlayerStateID, GameInitPlayerStateManagerData, TState>
        where TState : GameCore.States.BaseGameInitPlayerState
    {
        public override abstract GameInitPlayerStateID ConditionsBranch(GameInitPlayerStateManagerData manager_data, TState state);
    }
}

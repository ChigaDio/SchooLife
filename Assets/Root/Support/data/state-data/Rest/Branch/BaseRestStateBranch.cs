using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseRestStateBranch<TState, TDetailState> : BaseStateBranch<RestStateID, RestStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseRestState
        where TDetailState : BaseRestDetailStateBranch<TState>
    {
        public override abstract RestStateID ConditionsBranch(RestStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(RestStateID id);
    }
}

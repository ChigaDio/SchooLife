using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseDateStateBranch<TState, TDetailState> : BaseStateBranch<DateStateID, DateStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseDateState
        where TDetailState : BaseDateDetailStateBranch<TState>
    {
        public override abstract DateStateID ConditionsBranch(DateStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(DateStateID id);
    }
}

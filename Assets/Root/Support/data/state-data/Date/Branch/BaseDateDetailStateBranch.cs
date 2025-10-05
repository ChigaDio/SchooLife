using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseDateDetailStateBranch<TState> : BaseDetailStateBranch<DateStateID, DateStateManagerData, TState>
        where TState : GameCore.States.BaseDateState
    {
        public override abstract DateStateID ConditionsBranch(DateStateManagerData manager_data, TState state);
    }
}

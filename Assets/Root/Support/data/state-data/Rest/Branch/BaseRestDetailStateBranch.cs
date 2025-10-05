using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseRestDetailStateBranch<TState> : BaseDetailStateBranch<RestStateID, RestStateManagerData, TState>
        where TState : GameCore.States.BaseRestState
    {
        public override abstract RestStateID ConditionsBranch(RestStateManagerData manager_data, TState state);
    }
}

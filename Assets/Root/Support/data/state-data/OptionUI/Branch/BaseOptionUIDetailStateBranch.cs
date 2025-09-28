using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseOptionUIDetailStateBranch<TState> : BaseDetailStateBranch<OptionUIStateID, OptionUIStateManagerData, TState>
        where TState : GameCore.States.BaseOptionUIState
    {
        public override abstract OptionUIStateID ConditionsBranch(OptionUIStateManagerData manager_data, TState state);
    }
}

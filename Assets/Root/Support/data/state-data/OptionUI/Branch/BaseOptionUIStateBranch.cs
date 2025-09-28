using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseOptionUIStateBranch<TState, TDetailState> : BaseStateBranch<OptionUIStateID, OptionUIStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseOptionUIState
        where TDetailState : BaseOptionUIDetailStateBranch<TState>
    {
        public override abstract OptionUIStateID ConditionsBranch(OptionUIStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(OptionUIStateID id);
    }
}

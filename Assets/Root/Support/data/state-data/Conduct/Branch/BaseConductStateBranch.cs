using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseConductStateBranch<TState, TDetailState> : BaseStateBranch<ConductStateID, ConductStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseConductState
        where TDetailState : BaseConductDetailStateBranch<TState>
    {
        public override abstract ConductStateID ConditionsBranch(ConductStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(ConductStateID id);
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseConductDetailStateBranch<TState> : BaseDetailStateBranch<ConductStateID, ConductStateManagerData, TState>
        where TState : GameCore.States.BaseConductState
    {
        public override abstract ConductStateID ConditionsBranch(ConductStateManagerData manager_data, TState state);
    }
}

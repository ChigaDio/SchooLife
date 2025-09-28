using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleSceneDetailStateBranch<TState> : BaseDetailStateBranch<TitleSceneStateID, TitleSceneStateManagerData, TState>
        where TState : GameCore.States.BaseTitleSceneState
    {
        public override abstract TitleSceneStateID ConditionsBranch(TitleSceneStateManagerData manager_data, TState state);
    }
}

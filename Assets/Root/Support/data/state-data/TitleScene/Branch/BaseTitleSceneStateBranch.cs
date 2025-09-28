using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;
namespace GameCore.States.Branch
{
    public abstract class BaseTitleSceneStateBranch<TState, TDetailState> : BaseStateBranch<TitleSceneStateID, TitleSceneStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseTitleSceneState
        where TDetailState : BaseTitleSceneDetailStateBranch<TState>
    {
        public override abstract TitleSceneStateID ConditionsBranch(TitleSceneStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(TitleSceneStateID id);
    }
}

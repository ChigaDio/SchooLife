using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleSceneTitleSelectDetailStateBranch : BaseTitleSceneDetailStateBranch<TitleSceneTitleSelectState>
    {
        public override abstract TitleSceneStateID ConditionsBranch(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public abstract bool TitleSceneTitleSelect_to_GameStart03(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public abstract bool TitleSceneTitleSelect_to_Option04(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public abstract bool TitleSceneTitleSelect_to_Credit05(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public abstract bool TitleSceneTitleSelect_to_Exit10(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
    }
}

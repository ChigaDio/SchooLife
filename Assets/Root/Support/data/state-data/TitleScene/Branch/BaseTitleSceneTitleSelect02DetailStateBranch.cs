using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleSceneTitleSelect02DetailStateBranch : BaseTitleSceneTitleSelectDetailStateBranch
    {
        public override TitleSceneStateID ConditionsBranch(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state)
        {
            if (TitleSceneTitleSelect_to_GameStart03(manager_data, state))
                return TitleSceneStateID.GameStart03;
            if (TitleSceneTitleSelect_to_Option04(manager_data, state))
                return TitleSceneStateID.Option04;
            if (TitleSceneTitleSelect_to_Credit05(manager_data, state))
                return TitleSceneStateID.Credit05;
            if (TitleSceneTitleSelect_to_Exit10(manager_data, state))
                return TitleSceneStateID.Exit10;
            return TitleSceneStateID.None;
        }

        public override abstract bool TitleSceneTitleSelect_to_GameStart03(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public override abstract bool TitleSceneTitleSelect_to_Option04(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public override abstract bool TitleSceneTitleSelect_to_Credit05(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
        public override abstract bool TitleSceneTitleSelect_to_Exit10(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state);
    }
}

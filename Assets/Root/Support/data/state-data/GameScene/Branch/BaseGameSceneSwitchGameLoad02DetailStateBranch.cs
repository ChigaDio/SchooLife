using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGameSceneSwitchGameLoad02DetailStateBranch : BaseGameSceneSwitchGameLoadDetailStateBranch
    {
        public override GameSceneStateID ConditionsBranch(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state)
        {
            if (GameSceneSwitchGameLoad_to_InitGame03(manager_data, state))
                return GameSceneStateID.InitGame03;
            if (GameSceneSwitchGameLoad_to_LoadSaveData04(manager_data, state))
                return GameSceneStateID.LoadSaveData04;
            return GameSceneStateID.None;
        }

        public override abstract bool GameSceneSwitchGameLoad_to_InitGame03(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state);
        public override abstract bool GameSceneSwitchGameLoad_to_LoadSaveData04(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state);
    }
}

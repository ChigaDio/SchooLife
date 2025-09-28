using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseGameSceneSwitchGameLoadDetailStateBranch : BaseGameSceneDetailStateBranch<GameSceneSwitchGameLoadState>
    {
        public override abstract GameSceneStateID ConditionsBranch(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state);
        public abstract bool GameSceneSwitchGameLoad_to_InitGame03(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state);
        public abstract bool GameSceneSwitchGameLoad_to_LoadSaveData04(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state);
    }
}

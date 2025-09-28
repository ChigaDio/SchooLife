using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.SaveSystem;

namespace GameCore.States.Branch
{
    public class GameSceneSwitchGameLoad02DetailStateBranch : BaseGameSceneSwitchGameLoad02DetailStateBranch
    {
        public override bool GameSceneSwitchGameLoad_to_InitGame03(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state)
        {
            return SaveManagerCore.Instance.PlayerProgress.isPlayStart == false ;
        }

        public override bool GameSceneSwitchGameLoad_to_LoadSaveData04(GameSceneStateManagerData manager_data, GameSceneSwitchGameLoadState state)
        {
            return SaveManagerCore.Instance.PlayerProgress.isPlayStart == true;
        }

    }
}

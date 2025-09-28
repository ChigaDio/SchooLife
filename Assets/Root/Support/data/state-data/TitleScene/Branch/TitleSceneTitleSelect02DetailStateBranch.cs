using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class TitleSceneTitleSelect02DetailStateBranch : BaseTitleSceneTitleSelect02DetailStateBranch
    {
        public override bool TitleSceneTitleSelect_to_GameStart03(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state)
        {
            return manager_data.selectTitle == TitleCanvas.SelectTile.GameStart;
        }

        public override bool TitleSceneTitleSelect_to_Option04(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state)
        {
            return manager_data.selectTitle == TitleCanvas.SelectTile.Setting;
        }

        public override bool TitleSceneTitleSelect_to_Credit05(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state)
        {
            return manager_data.selectTitle == TitleCanvas.SelectTile.Credit;
        }

        public override bool TitleSceneTitleSelect_to_Exit10(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state)
        {
            return manager_data.selectTitle == TitleCanvas.SelectTile.Exit;
        }

    }
}

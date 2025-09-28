using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GamePlayFinishCheck10DetailStateBranch : BaseGamePlayFinishCheck10DetailStateBranch
    {
        public override bool GamePlayFinishCheck_to_FinishExit11(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state)
        {
            return false;
        }

        public override bool GamePlayFinishCheck_to_FadeIn01(GamePlayStateManagerData manager_data, GamePlayFinishCheckState state)
        {
            return false;
        }

    }
}

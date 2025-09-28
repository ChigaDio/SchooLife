using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class GamePlayEventCheck06DetailStateBranch : BaseGamePlayEventCheck06DetailStateBranch
    {
        public override bool GamePlayEventCheck_to_Event08(GamePlayStateManagerData manager_data, GamePlayEventCheckState state)
        {
            return false;
        }

        public override bool GamePlayEventCheck_to_Save09(GamePlayStateManagerData manager_data, GamePlayEventCheckState state)
        {
            return false;
        }

    }
}

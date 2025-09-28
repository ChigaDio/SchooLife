using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainLoopGame02DetailStateBranch : BaseMainLoopGame02DetailStateBranch
    {
        public override bool MainLoopGame_to_Exit03(MainLoopStateManagerData manager_data, MainLoopGameState state)
        {
            return false;
        }

        public override bool MainLoopGame_to_Title01(MainLoopStateManagerData manager_data, MainLoopGameState state)
        {
            return false;
        }

    }
}

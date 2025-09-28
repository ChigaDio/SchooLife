using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainLoopGame02DetailStateBranch : BaseMainLoopGameDetailStateBranch
    {
        public override MainLoopStateID ConditionsBranch(MainLoopStateManagerData manager_data, MainLoopGameState state)
        {
            if (MainLoopGame_to_Exit03(manager_data, state))
                return MainLoopStateID.Exit03;
            if (MainLoopGame_to_Title01(manager_data, state))
                return MainLoopStateID.Title01;
            return MainLoopStateID.None;
        }

        public override abstract bool MainLoopGame_to_Exit03(MainLoopStateManagerData manager_data, MainLoopGameState state);
        public override abstract bool MainLoopGame_to_Title01(MainLoopStateManagerData manager_data, MainLoopGameState state);
    }
}

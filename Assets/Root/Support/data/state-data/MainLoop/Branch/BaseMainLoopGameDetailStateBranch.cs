using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainLoopGameDetailStateBranch : BaseMainLoopDetailStateBranch<MainLoopGameState>
    {
        public override abstract MainLoopStateID ConditionsBranch(MainLoopStateManagerData manager_data, MainLoopGameState state);
        public abstract bool MainLoopGame_to_Exit03(MainLoopStateManagerData manager_data, MainLoopGameState state);
        public abstract bool MainLoopGame_to_Title01(MainLoopStateManagerData manager_data, MainLoopGameState state);
    }
}

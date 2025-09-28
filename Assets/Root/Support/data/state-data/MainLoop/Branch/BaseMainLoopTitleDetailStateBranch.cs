using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainLoopTitleDetailStateBranch : BaseMainLoopDetailStateBranch<MainLoopTitleState>
    {
        public override abstract MainLoopStateID ConditionsBranch(MainLoopStateManagerData manager_data, MainLoopTitleState state);
        public abstract bool MainLoopTitle_to_Game02(MainLoopStateManagerData manager_data, MainLoopTitleState state);
        public abstract bool MainLoopTitle_to_Exit03(MainLoopStateManagerData manager_data, MainLoopTitleState state);
    }
}

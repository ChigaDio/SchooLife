using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainLoopTitle01DetailStateBranch : BaseMainLoopTitleDetailStateBranch
    {
        public override MainLoopStateID ConditionsBranch(MainLoopStateManagerData manager_data, MainLoopTitleState state)
        {
            if (MainLoopTitle_to_Game02(manager_data, state))
                return MainLoopStateID.Game02;
            if (MainLoopTitle_to_Exit03(manager_data, state))
                return MainLoopStateID.Exit03;
            return MainLoopStateID.None;
        }

        public override abstract bool MainLoopTitle_to_Game02(MainLoopStateManagerData manager_data, MainLoopTitleState state);
        public override abstract bool MainLoopTitle_to_Exit03(MainLoopStateManagerData manager_data, MainLoopTitleState state);
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainLoopTitle01DetailStateBranch : BaseMainLoopTitle01DetailStateBranch
    {
        public override bool MainLoopTitle_to_Game02(MainLoopStateManagerData manager_data, MainLoopTitleState state)
        {
            return manager_data.IsExit() == false;
        }

        public override bool MainLoopTitle_to_Exit03(MainLoopStateManagerData manager_data, MainLoopTitleState state)
        {
            return manager_data.IsExit() == true;
        }

    }
}

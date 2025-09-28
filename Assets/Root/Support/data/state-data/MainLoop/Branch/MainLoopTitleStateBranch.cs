using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainLoopTitleStateBranch : BaseMainLoopStateBranch<MainLoopTitleState, BaseMainLoopTitleDetailStateBranch>
    {
        public override MainLoopStateID ConditionsBranch(MainLoopStateManagerData manager_data, MainLoopTitleState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : MainLoopStateID.None;
        }

        public override BaseMainLoopTitleDetailStateBranch Factory(MainLoopStateID id)
        {
            switch (id)
            {
                case MainLoopStateID.Title01:
                    return new MainLoopTitle01DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

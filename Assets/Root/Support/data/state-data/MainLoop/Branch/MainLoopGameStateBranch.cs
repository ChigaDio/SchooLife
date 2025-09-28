using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainLoopGameStateBranch : BaseMainLoopStateBranch<MainLoopGameState, BaseMainLoopGameDetailStateBranch>
    {
        public override MainLoopStateID ConditionsBranch(MainLoopStateManagerData manager_data, MainLoopGameState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : MainLoopStateID.None;
        }

        public override BaseMainLoopGameDetailStateBranch Factory(MainLoopStateID id)
        {
            switch (id)
            {
                case MainLoopStateID.Game02:
                    return new MainLoopGame02DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

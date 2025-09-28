using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class TitleSceneTitleSelectStateBranch : BaseTitleSceneStateBranch<TitleSceneTitleSelectState, BaseTitleSceneTitleSelectDetailStateBranch>
    {
        public override TitleSceneStateID ConditionsBranch(TitleSceneStateManagerData manager_data, TitleSceneTitleSelectState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : TitleSceneStateID.None;
        }

        public override BaseTitleSceneTitleSelectDetailStateBranch Factory(TitleSceneStateID id)
        {
            switch (id)
            {
                case TitleSceneStateID.TitleSelect02:
                    return new TitleSceneTitleSelect02DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

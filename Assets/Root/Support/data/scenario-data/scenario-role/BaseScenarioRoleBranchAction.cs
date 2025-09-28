
using System;
using System.Collections;
using UnityEngine;

namespace GameCore.Scenario
{
    public class BaseScenarioRoleBranchAction<T> : BaseGeneralScenarioRoleAction<T> where T : BaseScenarioRoleData
    {


        public BaseScenarioRoleBranchAction(T roleData) : base(roleData)
        {

        }

        public override void OnInitialize()
        {
            base.OnInitialize();
        }


    }
}

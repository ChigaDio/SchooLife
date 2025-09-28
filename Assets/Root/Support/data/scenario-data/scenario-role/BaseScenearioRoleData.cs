
using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace GameCore.Scenario
{
    public class BaseScenarioRoleData
    {
        public ScenarioRoleID RoleID { get; private set; }
        public int ScenarioGroupID { get; private set; }
        public int ScenarioSubGroupID { get; private set; }
        public int ScenarioSeekPos { get; set; } = -1;

        public virtual void ReadBinary(BinaryReader reader)
        {
            RoleID = (ScenarioRoleID)reader.ReadInt32();
            ScenarioGroupID = reader.ReadInt32();
            ScenarioSubGroupID = reader.ReadInt32();
        }
    }
}

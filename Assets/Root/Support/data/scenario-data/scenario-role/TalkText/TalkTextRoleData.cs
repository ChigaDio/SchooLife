using System;
using System.IO;
using UnityEngine;

namespace GameCore.Scenario {
    public class TalkTextRoleData : BaseScenarioRoleData {
        
        public string text { get; set; }
        public string name { get; set; }
        
        public TalkTextRoleData()
        {
            RoleID = ScenarioRoleID.TalkText;
        }
           public override void ReadBinary(BinaryReader reader) {
            text = reader.ReadString();
            name = reader.ReadString();
    }
  }
}

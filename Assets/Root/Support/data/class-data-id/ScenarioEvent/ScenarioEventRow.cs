using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public class ScenarioEventRow : BaseClassDataRow
    {
            private string eventID;
            public string Eventid { get => eventID; }
            private string subID;
            public string Subid { get => subID; }

            public override void Read(BinaryReader reader)
            {
                int len0 = reader.ReadInt32();
                eventID = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len0));
                int len1 = reader.ReadInt32();
                subID = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len1));
            }
        }

}


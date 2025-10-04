using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public class ActionExecuteCommandRow : BaseClassDataRow
    {
            private string jpName;
            public string Jpname { get => jpName; }
            private string enName;
            public string Enname { get => enName; }

            public override void Read(BinaryReader reader)
            {
                int len0 = reader.ReadInt32();
                jpName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len0));
                int len1 = reader.ReadInt32();
                enName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len1));
            }
        }

}


using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public class PersonalityRow : BaseClassDataRow
    {
            private bool use;
            public bool Use { get => use; }
            private string jpText;
            public string Jptext { get => jpText; }

            public override void Read(BinaryReader reader)
            {
                use = reader.ReadBoolean();
                int len1 = reader.ReadInt32();
                jpText = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len1));
            }
        }

}


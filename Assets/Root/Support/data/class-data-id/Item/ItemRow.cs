using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public class ItemRow : BaseClassDataRow
    {
            private bool use;
            public bool Use { get => use; }

            public override void Read(BinaryReader reader)
            {
                use = reader.ReadBoolean();
            }
        }

}


using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public class ActionExecuteCommandTable : BaseClassDataID<ActionExecuteCommandTableID, ActionExecuteCommandRow>
    {
        public override void Read(BinaryReader reader)
        {
            ActionExecuteCommandTable.Table.Clear();
            int rowCount = reader.ReadInt32();
            int colCount = reader.ReadInt32();
            var colNames = new string[colCount];
            var colTypes = new string[colCount];
            for(int i=0; i<colCount; i++) {
                int len = reader.ReadInt32();
                colNames[i] = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len));
                len = reader.ReadInt32();
                colTypes[i] = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len));
            }
            for(int r=0; r<rowCount; r++) {
                var enumVal = (ActionExecuteCommandTableID)Enum.ToObject(typeof(ActionExecuteCommandTableID), reader.ReadInt32());
                var row = new ActionExecuteCommandRow();
                row.Read(reader);
                Table[enumVal] = row;
            }
        }
    }
}

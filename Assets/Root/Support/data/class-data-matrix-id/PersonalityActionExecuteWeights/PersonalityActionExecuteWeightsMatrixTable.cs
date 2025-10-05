using System.IO;
using GameCore.Tables.ID;
using GameCore.Enums;
using System;
using System.Collections.Generic;

namespace GameCore.Tables {
    public class PersonalityActionExecuteWeightsMatrixTable : BaseClassDataMatrixID<PersonalityTableID, ActionExecuteCommandTableID, PersonalityActionExecuteWeightsMatrixRow> {
        public override void Read(BinaryReader reader) {
            PersonalityActionExecuteWeightsMatrixTable.Table.Clear();
            int rowCount = reader.ReadInt32();
            List<PersonalityTableID> rowKeys = new List<PersonalityTableID>(); for(int i=0; i<rowCount; i++) rowKeys.Add((PersonalityTableID)reader.ReadInt32());
            int colCount = reader.ReadInt32();
            List<ActionExecuteCommandTableID> colKeys = new List<ActionExecuteCommandTableID>(); for(int i=0; i<colCount; i++) colKeys.Add((ActionExecuteCommandTableID)reader.ReadInt32());
            foreach(var rk in rowKeys) { Table[rk] = new Dictionary<ActionExecuteCommandTableID, PersonalityActionExecuteWeightsMatrixRow>(); }
            foreach(var rk in rowKeys) { foreach(var ck in colKeys) { var row = new PersonalityActionExecuteWeightsMatrixRow(); row.Read(reader); Table[rk][ck] = row; } }
        }
    }
}

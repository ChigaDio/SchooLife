using System.IO;
using GameCore.Tables.ID;
using GameCore.Enums;
using System;
using System.Collections.Generic;

namespace GameCore.Tables {
    public class PersonalityStatWeightsMatrixTable : BaseClassDataMatrixID<PersonalityTableID, CharacterStatID, PersonalityStatWeightsMatrixRow> {
        public override void Read(BinaryReader reader) {
            PersonalityStatWeightsMatrixTable.Table.Clear();
            int rowCount = reader.ReadInt32();
            List<PersonalityTableID> rowKeys = new List<PersonalityTableID>(); for(int i=0; i<rowCount; i++) rowKeys.Add((PersonalityTableID)reader.ReadInt32());
            int colCount = reader.ReadInt32();
            List<CharacterStatID> colKeys = new List<CharacterStatID>(); for(int i=0; i<colCount; i++) colKeys.Add((CharacterStatID)reader.ReadInt32());
            foreach(var rk in rowKeys) { Table[rk] = new Dictionary<CharacterStatID, PersonalityStatWeightsMatrixRow>(); }
            foreach(var rk in rowKeys) { foreach(var ck in colKeys) { var row = new PersonalityStatWeightsMatrixRow(); row.Read(reader); Table[rk][ck] = row; } }
        }
    }
}

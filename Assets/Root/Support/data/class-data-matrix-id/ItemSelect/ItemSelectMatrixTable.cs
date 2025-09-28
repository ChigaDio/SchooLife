using System.IO;
using GameCore.Tables.ID;
using GameCore.Enums;
using System;
using System.Collections.Generic;

namespace GameCore.Tables {
    public class ItemSelectMatrixTable : BaseClassDataMatrixID<ItemTableID, ItemLevelID, ItemSelectMatrixRow> {
        public override void Read(BinaryReader reader) {
            ItemSelectMatrixTable.Table.Clear();
            int rowCount = reader.ReadInt32();
            List<ItemTableID> rowKeys = new List<ItemTableID>(); for(int i=0; i<rowCount; i++) rowKeys.Add((ItemTableID)reader.ReadInt32());
            int colCount = reader.ReadInt32();
            List<ItemLevelID> colKeys = new List<ItemLevelID>(); for(int i=0; i<colCount; i++) colKeys.Add((ItemLevelID)reader.ReadInt32());
            foreach(var rk in rowKeys) { Table[rk] = new Dictionary<ItemLevelID, ItemSelectMatrixRow>(); }
            foreach(var rk in rowKeys) { foreach(var ck in colKeys) { var row = new ItemSelectMatrixRow(); row.Read(reader); Table[rk][ck] = row; } }
        }
    }
}

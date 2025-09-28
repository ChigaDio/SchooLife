using System.IO;
using System;
using System.Collections.Generic;

namespace GameCore.Tables {
    public class ItemSelectMatrixRow : BaseClassDataMatrixRow {
        private float point;
        public float Point { get => point; } // 入手ポイント
        private float value;
        public float Value { get => value; } // 能力
        public override void Read(BinaryReader reader) {
            point = reader.ReadSingle();
            value = reader.ReadSingle();
        }
    }
}

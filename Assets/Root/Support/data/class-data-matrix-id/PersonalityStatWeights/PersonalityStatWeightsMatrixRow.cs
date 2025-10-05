using System.IO;
using System;
using System.Collections.Generic;

namespace GameCore.Tables {
    public class PersonalityStatWeightsMatrixRow : BaseClassDataMatrixRow {
        private float RiseFactor;
        public float Risefactor { get => RiseFactor; } // 上昇倍率
        private float ActionProbability;
        public float Actionprobability { get => ActionProbability; } // 実行確率
        public override void Read(BinaryReader reader) {
            RiseFactor = reader.ReadSingle();
            ActionProbability = reader.ReadSingle();
        }
    }
}

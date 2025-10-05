using System.IO;
using System;
using System.Collections.Generic;

namespace GameCore.Tables {
    public class PersonalityActionExecuteWeightsMatrixRow : BaseClassDataMatrixRow {
        private float ActionProbability ;
        public float Actionprobability  { get => ActionProbability ; } // 選ぶ確率
        public override void Read(BinaryReader reader) {
            ActionProbability  = reader.ReadSingle();
        }
    }
}

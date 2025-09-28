using System.IO;
    using System;
    using System.Collections.Generic;

    namespace GameCore.Tables
    {
        public abstract class BaseClassDataMatrixID<TRow, TCol, E> : BaseTableMatrix where TRow : Enum where TCol : Enum where E : BaseClassDataMatrixRow
        {
            public static Dictionary<TRow, Dictionary<TCol, E>> Table = new Dictionary<TRow, Dictionary<TCol, E>>();
            public override abstract void Read(BinaryReader reader);
            public override void Release()
            {
                Table.Clear();
            }
        }
    }

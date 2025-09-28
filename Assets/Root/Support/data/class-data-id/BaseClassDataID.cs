using System.IO;
    using System;
    using System.Collections.Generic;

    namespace GameCore.Tables
    {
        public abstract class BaseClassDataID<T,E> : BaseTable where T : Enum where E : BaseClassDataRow
        {
            public static Dictionary<T,E> Table = new Dictionary<T,E>();
            public override abstract void Read(BinaryReader reader);
            public override void Release()
            {
                Table.Clear();
            }
        }
    }

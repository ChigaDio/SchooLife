using System.IO;

    namespace GameCore.Tables
    {
        public abstract class BaseClassDataRow
        {
            public abstract void Read(BinaryReader reader);
        }
    }

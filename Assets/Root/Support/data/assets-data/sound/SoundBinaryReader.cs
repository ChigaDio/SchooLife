
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameCore.Sound
{
    public class SoundBinaryReader
    {
        public static SoundDatabase LoadSoundDatabaseFromBinary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"Binary file not found: {filePath}");
                return null;
            }

            SoundDatabase database = new SoundDatabase();

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int groupCount = reader.ReadInt32();
                int[] offsets = new int[groupCount];

                for (int i = 0; i < groupCount; i++)
                {
                    offsets[i] = reader.ReadInt32();
                }

                string[] groupNames = Enum.GetNames(typeof(SoundGroup));
                if (groupCount > groupNames.Length - 1)
                {
                    Debug.LogError("Binary contains more groups than defined in SoundGroup enum.");
                    return null;
                }

                for (int i = 0; i < groupCount; i++)
                {
                    reader.BaseStream.Seek(offsets[i], SeekOrigin.Begin);
                    int soundCount = reader.ReadInt32();
                    List<SoundDatabase.SoundData> sounds = new List<SoundDatabase.SoundData>();

                    for (int j = 0; j < soundCount; j++)
                    {
                        int id = reader.ReadInt32();
                        string addressablePath = ReadNullTerminatedString(reader);
                        float volume = reader.ReadSingle();
                        byte typeByte = reader.ReadByte();
                        SoundType type = (typeByte == 0) ? SoundType.SE : SoundType.BGM;

                        string groupName = groupNames[i + 1];
                        string enumName = Enum.GetName(typeof(SoundID), id) ?? $"Unknown_{id}";
                        sounds.Add(new SoundDatabase.SoundData(
                            idName: enumName, // Store only the sound name
                            addressablePath: addressablePath,
                            baseVolume: volume,
                            type: type,
                            soundID: (SoundID)id
                        ));
                    }

                    database.GroupedSoundsList.Add(new SoundDatabase.GroupedSounds(
                        group: (SoundGroup)(i + 1),
                        sounds: sounds
                    ));
                }
            }

            return database;
        }

        private static string ReadNullTerminatedString(BinaryReader reader)
        {
            List<byte> bytes = new List<byte>();
            byte b;
            while ((b = reader.ReadByte()) != 0)
            {
                bytes.Add(b);
            }
            return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
        
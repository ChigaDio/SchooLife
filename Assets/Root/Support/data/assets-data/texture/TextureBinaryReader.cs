using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameCore.Enums;
namespace GameCore.Texture
{
    public class TextureBinaryReader
    {
        public static TextureDatabase LoadTextureDatabaseFromBinary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"Binary file not found: {filePath}");
                return null;
            }

            TextureDatabase database = new TextureDatabase();

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int groupCount = reader.ReadInt32();
                int[] offsets = new int[groupCount];

                for (int i = 0; i < groupCount; i++)
                {
                    offsets[i] = reader.ReadInt32();
                }

                string[] groupNames = Enum.GetNames(typeof(TextureGroup));
                if (groupCount > groupNames.Length - 1)
                {
                    Debug.LogError("Binary contains more groups than defined in TextureGroup enum.");
                    return null;
                }

                for (int i = 0; i < groupCount; i++)
                {
                    reader.BaseStream.Seek(offsets[i], SeekOrigin.Begin);
                    int textureCount = reader.ReadInt32();
                    List<TextureDatabase.TextureData> textures = new List<TextureDatabase.TextureData>();

                    for (int j = 0; j < textureCount; j++)
                    {
                        int textureId = reader.ReadInt32();
                        string textureIdName = ReadNullTerminatedString(reader);
                        string addressablePath = ReadNullTerminatedString(reader);
                        bool isSpriteSheet = reader.ReadBoolean();
                        int spriteCount = reader.ReadInt32();
                        List<TextureDatabase.SpriteData> sprites = new List<TextureDatabase.SpriteData>();

                        for (int k = 0; k < spriteCount; k++)
                        {
                            int spriteTextureId = reader.ReadInt32();
                            string spriteIdName = ReadNullTerminatedString(reader);
                            string spriteAddressablePath = ReadNullTerminatedString(reader);

                            sprites.Add(new TextureDatabase.SpriteData(
                                textureID: (TextureID)spriteTextureId,
                                idName: spriteIdName,
                                addressablePath: spriteAddressablePath
                            ));
                        }

                        textures.Add(new TextureDatabase.TextureData(
                            textureID: (TextureID)textureId,
                            idName: textureIdName,
                            addressablePath: addressablePath,
                            sprites: sprites,
                            isSpriteSheet: isSpriteSheet
                        ));
                    }

                    database.GroupedTexturesList.Add(new TextureDatabase.GroupedTextures(
                        group: (TextureGroup)(i + 1),
                        textures: textures
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
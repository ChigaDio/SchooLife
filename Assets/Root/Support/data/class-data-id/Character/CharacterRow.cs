using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GameCore.Tables.ID;
using GameCore.Enums;
namespace GameCore.Tables
{
    public class CharacterRow : BaseClassDataRow
    {
            private string jpFamilyName;
            public string Jpfamilyname { get => jpFamilyName; }
            private string jpFirstName;
            public string Jpfirstname { get => jpFirstName; }
            private string enFamilyName;
            public string Enfamilyname { get => enFamilyName; }
            private string enFirstName;
            public string Enfirstname { get => enFirstName; }
            private GameCore.Enums.TextureID textureID;
            public GameCore.Enums.TextureID Textureid { get => textureID; }
            private GameCore.Enums.Game_CharacterList01ID spriteID;
            public GameCore.Enums.Game_CharacterList01ID Spriteid { get => spriteID; }
            private bool use;
            public bool Use { get => use; }

            public override void Read(BinaryReader reader)
            {
                int len0 = reader.ReadInt32();
                jpFamilyName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len0));
                int len1 = reader.ReadInt32();
                jpFirstName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len1));
                int len2 = reader.ReadInt32();
                enFamilyName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len2));
                int len3 = reader.ReadInt32();
                enFirstName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len3));
                textureID = (GameCore.Enums.TextureID)Enum.ToObject(typeof(GameCore.Enums.TextureID), reader.ReadInt32());
                spriteID = (GameCore.Enums.Game_CharacterList01ID)Enum.ToObject(typeof(GameCore.Enums.Game_CharacterList01ID), reader.ReadInt32());
                use = reader.ReadBoolean();
            }
        }

}


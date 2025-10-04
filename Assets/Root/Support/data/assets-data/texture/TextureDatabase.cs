
using System.Collections.Generic;
using GameCore.Enums;
namespace GameCore.Texture
{
    public class TextureDatabase
    {
        [System.Serializable]
        public class SpriteData
        {
            private readonly string idName;
            private readonly string addressablePath;
            private readonly TextureID textureID;
            public SpriteData(TextureID textureID, string idName, string addressablePath)
            {
                this.idName = idName;
                this.addressablePath = addressablePath;
                this.textureID = textureID;
            }
            public string IdName => idName;
            public string AddressablePath => addressablePath;
            public TextureID TextureID => textureID;
        }

        [System.Serializable]
        public class TextureData
        {
            private readonly string idName;
            private readonly string addressablePath;
            private readonly TextureID textureID;
            private readonly List<SpriteData> sprites;
            private readonly bool isSpriteSheet;
            public TextureData(TextureID textureID, string idName, string addressablePath, List<SpriteData> sprites, bool isSpriteSheet)
            {
                this.idName = idName;
                this.addressablePath = addressablePath;
                this.textureID = textureID;
                this.sprites = sprites ?? new List<SpriteData>();
                this.isSpriteSheet = isSpriteSheet;
            }
            public string IdName => idName;
            public string AddressablePath => addressablePath;
            public TextureID TextureID => textureID;
            public List<SpriteData> Sprites => sprites;
            public bool IsSpriteSheet => isSpriteSheet;
        }

        [System.Serializable]
        public class GroupedTextures
        {
            private readonly TextureGroup group;
            private readonly List<TextureData> textures;
            public GroupedTextures(TextureGroup group, List<TextureData> textures)
            {
                this.group = group;
                this.textures = textures ?? new List<TextureData>();
            }
            public TextureGroup Group => group;
            public List<TextureData> Textures => textures;
        }

        private readonly List<GroupedTextures> groupedTextures;
        public TextureDatabase()
        {
            groupedTextures = new List<GroupedTextures>();
        }
        public List<GroupedTextures> GroupedTexturesList => groupedTextures;
    }
}

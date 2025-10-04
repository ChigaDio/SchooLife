
using System;
using System.Collections.Generic;
using UnityEngine;
using AddressableSystem;
using Cysharp.Threading.Tasks;

namespace GameCore.Texture
{
    public class TextureAddressableData
    {
        private readonly AddressableData<Sprite> spriteData;
        private readonly AddressableData<Texture2D> textureData;
        private readonly bool isSprite;

        public TextureAddressableData(GroupCategory groupCategory, AssetCategory assetCategory, bool isSprite)
        {
            this.isSprite = isSprite;
            if (isSprite)
            {
                spriteData = new AddressableData<Sprite>(groupCategory, assetCategory);
                textureData = null;
            }
            else
            {
                textureData = new AddressableData<Texture2D>(groupCategory, assetCategory);
                spriteData = null;
            }
        }

        public bool IsLoadedAndSetup => isSprite ? spriteData.IsLoadedAndSetup : textureData.IsLoadedAndSetup;

        public async UniTask LoadAsync(string path, int spriteCount, Action<object> onSuccess, Action<Exception> onError)
        {
            if (isSprite)
            {
                if (spriteCount > 1)
                {
                    await spriteData.LoadArrayAsync(path, sprites =>
                    {
                        onSuccess?.Invoke(sprites);
                    }, onError);
                }
                else
                {
                    await spriteData.LoadAsync(path, spr =>
                    {
                        onSuccess?.Invoke(spr);
                    }, onError);
                }
            }
            else
            {
                await textureData.LoadAsync(path, tex => onSuccess?.Invoke(tex), onError);
            }
        }

        public void Release()
        {
            if (isSprite)
            {
                spriteData?.Release();
            }
            else
            {
                textureData?.Release();
            }
        }

        public object GetAddressableObjectResult()
        {
            return isSprite ? spriteData.GetAddressableObjectResult() : textureData.GetAddressableObjectResult();
        }

        public Sprite[] GetAddressableArrayResult()
        {
            if (isSprite)
            {
                return spriteData.GetAddressableObjectResult() is Sprite sprite ? new[] { sprite } : spriteData.GetAddressableObjectArrayResult();
            }
            return null;
        }
    }
}
        
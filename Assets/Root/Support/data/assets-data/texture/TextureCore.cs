
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using AddressableSystem;
using GameCore.Enums;

namespace GameCore.Texture
{
    public class TextureCore : BaseSingleton<TextureCore>
    {
        private TextureDatabase database;
        private Dictionary<TextureGroup, Dictionary<TextureID, TextureAddressableData>> loadedAssets =
            new Dictionary<TextureGroup, Dictionary<TextureID, TextureAddressableData>>();
        private bool isLoadDatabase = false;
        public bool IsLoadDatabase => isLoadDatabase;
        private CancellationToken destroyToken;

        public override void AwakeSingleton()
        {
            base.AwakeSingleton();
            instance = this;
            DontDestroyOnLoad(gameObject);
            destroyToken = this.GetCancellationTokenOnDestroy();
            LoadDatabaseAsync().Forget();
        }

        private async UniTask LoadDatabaseAsync()
        {
            database = TextureBinaryReader.LoadTextureDatabaseFromBinary(SupportFiles.ALL_TEXTURE_BIN);
            if (database == null)
            {
                Debug.LogError("Failed to load TextureDatabase from binary.");
            }
            await UniTask.CompletedTask.AttachExternalCancellation(destroyToken);
            isLoadDatabase = true;
        }

        public void LoadGroup(TextureGroup group, GroupCategory groupCategory, Action action = null)
        {
            LoadGroupAsync(group, groupCategory, action).Forget();
        }

        public async UniTask LoadGroupAsync(TextureGroup group, GroupCategory groupCategory, Action action = null)
        {
            while (database == null)
            {
                await UniTask.Yield(cancellationToken: destroyToken);
            }
            if (loadedAssets.ContainsKey(group)) return;
            var textures = database.GroupedTexturesList.FirstOrDefault(data => data.Group == group);
            if (textures == null) return;

            loadedAssets[group] = new Dictionary<TextureID, TextureAddressableData>();
            var tasks = new List<UniTask>();

            foreach (var texture in textures.Textures)
            {
                if (texture.IsSpriteSheet)
                {
                    // スプライトシート自体のロード
                    var addressableSpriteSheet = new TextureAddressableData(groupCategory, AssetCategory.Sprite, true);
                    tasks.Add(addressableSpriteSheet.LoadAsync(texture.AddressablePath, texture.Sprites.Count, obj =>
                    {
                        if (addressableSpriteSheet.IsLoadedAndSetup)
                        {
                            loadedAssets[group][texture.TextureID] = addressableSpriteSheet;
                        }
                    }, ex =>
                    {
                        Debug.LogError($"Failed to load sprite sheet for {texture.TextureID} at {texture.AddressablePath}: {ex.Message}");
                    }).AttachExternalCancellation(destroyToken));




                }
                else
                {
                    // テクスチャのロード
                    var addressableTexture = new TextureAddressableData(groupCategory, AssetCategory.Texture, false);
                    tasks.Add(addressableTexture.LoadAsync(texture.AddressablePath, 0, obj =>
                    {
                        if (addressableTexture.IsLoadedAndSetup)
                        {
                            loadedAssets[group][texture.TextureID] = addressableTexture;
                        }
                    }, ex =>
                    {
                        Debug.LogError($"Failed to load texture for {texture.TextureID} at {texture.AddressablePath}: {ex.Message}");
                    }).AttachExternalCancellation(destroyToken));
                }
            }

            await UniTask.WhenAll(tasks);
            action?.Invoke();
        }

        public void UnloadGroup(TextureGroup group, GroupCategory groupCategory, Action action = null)
        {
            UnloadGroupAsync(group, groupCategory, action).Forget();
        }

        public async UniTask UnloadGroupAsync(TextureGroup group, GroupCategory groupCategory, Action action = null)
        {
            if (!loadedAssets.TryGetValue(group, out var assets)) return;

            foreach (var addressable in assets.Values)
            {
                addressable.Release();
            }
            loadedAssets.Remove(group);
            AddressableDataCore.Instance.ReleaseCategory(groupCategory, AssetCategory.Texture);
            AddressableDataCore.Instance.ReleaseCategory(groupCategory, AssetCategory.Sprite);
            action?.Invoke();
            await UniTask.CompletedTask.AttachExternalCancellation(destroyToken);
        }

        

        public Texture2D GetTexture(TextureGroup group, TextureID id)
        {
            if (loadedAssets.TryGetValue(group, out var groupAssets) && groupAssets.TryGetValue(id, out var addressable))
            {
                var result = addressable.GetAddressableObjectResult();
                if (result is Texture2D texture)
                {
                    return texture;
                }
                Debug.LogWarning($"Asset with ID {id} in group {group} is not a Texture2D.");
            }
            return null;
        }

        public Sprite GetSprite(TextureGroup group, TextureID id)
        {
            if (loadedAssets.TryGetValue(group, out var groupAssets) && groupAssets.TryGetValue(id, out var addressable))
            {
                var result = addressable.GetAddressableObjectResult();
                if (result is Sprite sprite)
                {
                    return sprite;
                }
                if (result is IList<Sprite> sprites && sprites.Count > 0)
                {
                    return sprites[0];
                }
                Debug.LogWarning($"Asset with ID {id} in group {group} is not a Sprite or Sprite array.");
            }
            return null;
        }

        public Sprite GetSprite<TEnum>(TextureGroup group, TextureID textureId, TEnum spriteIndex, int fallbackIndex = -1) where TEnum : Enum
        {
            if (loadedAssets.TryGetValue(group, out var groupAssets) &&
                groupAssets.TryGetValue(textureId, out var addressable))
            {
                // TEnum を数値（インデックス）として変換
                int index = Convert.ToInt32(spriteIndex);

                // fallbackIndex が指定されている場合はそちらを優先
                int targetIndex = fallbackIndex >= 0 ? fallbackIndex : index;

                var result = addressable.GetAddressableObjectResult();
                if (result is Sprite sprite && targetIndex == 0)
                {
                    return sprite; // 単一スプライトの場合
                }
                if (result is IList<Sprite> sprites && sprites.Count > 0)
                {
                    if (targetIndex >= 0 && targetIndex < sprites.Count)
                    {
                        return sprites[targetIndex];
                    }
                    return sprites[0];
                }
                Debug.LogWarning($"Asset with ID {textureId} in group {group} is not a Sprite or Sprite array.");
            }
            Debug.LogWarning($"No asset found for TextureID {textureId} in group {group}.");
            return null;
        }

        private void OnDestroy()
        {
            foreach (var group in loadedAssets.Values)
            {
                foreach (var asset in group.Values)
                {
                    asset.Release();
                }
            }
            loadedAssets.Clear();
        }
    }
}

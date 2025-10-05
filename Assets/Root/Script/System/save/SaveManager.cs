using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameCore.Enums;
using GameCore.Tables;
using GameCore.Tables.ID;
using UnityEngine;

namespace GameCore.SaveSystem
{
    [Serializable]
    public class SystemData
    {
        public float seVolume = 1.0f;
        public float bgmVolume = 1.0f;
    }

    [Serializable]
    public class ItemData
    {
        public ItemLevelID levelID;
    }

    [Serializable]
    public class ItemListData
    {
        public Dictionary<ItemTableID, ItemData> itemList = new Dictionary<ItemTableID, ItemData>();
    }

    [Serializable]
    public class PlayerData
    {
        // プレイヤーデータのフィールドは後で定義。拡張性を確保。
        public string familyName = "";
        public string firstName = "";
        public PersonalityTableID personalityTableID = PersonalityTableID.None;

        public int studyNum = 0;
        public int staminaNum = 0;
        public int appearanceNum = 0;

        /// <summary>
        /// 現在のターン数
        /// </summary>
        public TurnTableID nowTurn = TurnTableID.Turn_31;

        public int health = 0;

        public int loopCount = 1;

        public bool isPlayStart = false;

        public ItemListData itemListData;

        public int PlaterStatusNum(CharacterStatID id)
        {
            return id switch
            {
                CharacterStatID.None => 0,
                CharacterStatID.Study => studyNum,
                CharacterStatID.Appearance => appearanceNum,
                CharacterStatID.Stamina => staminaNum,
                _ => 0,

            };
        }

        public void AddPlayerStatusNum(CharacterStatID id,int add)
        {
            _ = id switch
            {
                CharacterStatID.None => 0,
                CharacterStatID.Study => studyNum += add,
                CharacterStatID.Appearance => appearanceNum += add,
                CharacterStatID.Stamina => staminaNum += add,
                _ => 0,

            };
        }

        public PlayerData()
        {
            if(itemListData == null)
            {
                itemListData = new ItemListData();
                for(ItemTableID item = ItemTableID.None + 1; item < ItemTableID.Max;item++)
                {
                    if (itemListData.itemList.ContainsKey(item)) continue;
                    var add = new ItemData();
                    add.levelID = ItemLevelID.None;
                    itemListData.itemList.Add(item, add);
                }
            }
        }
    }

    public class SaveManager
    {
        private readonly string systemDataPath;
        private readonly string playerDataPath;
        private readonly CancellationTokenSource cts;
        private readonly byte[] encryptionKey = { 0xA1, 0xB2, 0xC3, 0xD4, 0xE5, 0xF6, 0x07, 0x18 };
        public SystemData SystemSettings { get; private set; } = new SystemData();
        public PlayerData PlayerProgress { get; private set; } = new PlayerData();
        public bool IsSaving { get; private set; }
        public bool IsLoading { get; private set; }

        public SaveManager(GameObject linkedGameObject)
        {
            string saveDir;
#if UNITY_EDITOR
            saveDir = Path.Combine(Application.dataPath, "SaveData");
#else
            saveDir = Path.Combine(Application.dataPath, "SaveData");
#endif
            Directory.CreateDirectory(saveDir);

            systemDataPath = Path.Combine(saveDir, "systemData.bin");
            playerDataPath = Path.Combine(saveDir, "playerData.bin");

            cts = new CancellationTokenSource();
            if (linkedGameObject != null)
            {
                CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token);
                linkedGameObject.GetCancellationTokenOnDestroy().Register(() => linkedCts.Cancel());
            }
        }

        private byte[] EncryptDecrypt(byte[] data)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ encryptionKey[i % encryptionKey.Length]);
            }
            return result;
        }

        private byte[] SerializeToBinary<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, data);
                return ms.ToArray();
            }
        }

        private T DeserializeFromBinary<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(ms);
            }
        }

        public async UniTask LoadAllDataAsync(Action onComplete = null)
        {
            try
            {
                IsLoading = true;
                await UniTask.WhenAll(
                    LoadSystemDataAsync(),
                    LoadPlayerDataAsync()
                );
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("LoadAllDataAsyncがキャンセルされました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"LoadAllDataAsyncでエラー: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                onComplete?.Invoke();
            }
        }

        public async UniTask SaveAllDataAsync(Action onComplete = null)
        {
            try
            {
                IsSaving = true;
                await UniTask.WhenAll(
                    SaveSystemDataAsync(),
                    SavePlayerDataAsync()
                );
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("SaveAllDataAsyncがキャンセルされました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"SaveAllDataAsyncでエラー: {ex.Message}");
            }
            finally
            {
                IsSaving = false;
                onComplete?.Invoke();
            }
        }

        public async UniTask LoadSystemDataAsync(Action onComplete = null)
        {
            try
            {
                IsLoading = true;
                await UniTask.RunOnThreadPool(() =>
                {
                    if (File.Exists(systemDataPath))
                    {
                        byte[] encryptedData = File.ReadAllBytes(systemDataPath);
                        byte[] decryptedData = EncryptDecrypt(encryptedData);
                        SystemSettings = DeserializeFromBinary<SystemData>(decryptedData) ?? new SystemData();
                        Debug.Log($"システムデータを読み込みました: {systemDataPath}");
                    }
                    else
                    {
                        SystemSettings = new SystemData();
                        SaveSystemDataAsync().Forget();
                        Debug.Log($"システムデータファイルが見つかりませんでした。デフォルトを使用: {systemDataPath}");
                    }
                }, cancellationToken: cts.Token);
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("LoadSystemDataAsyncがキャンセルされました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"LoadSystemDataAsyncでエラー: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                onComplete?.Invoke();
            }
        }

        public async UniTask SaveSystemDataAsync(Action onComplete = null)
        {
            try
            {
                IsSaving = true;
                await UniTask.RunOnThreadPool(() =>
                {
                    byte[] data = SerializeToBinary(SystemSettings);
                    byte[] encryptedData = EncryptDecrypt(data);
                    File.WriteAllBytes(systemDataPath, encryptedData);
                    Debug.Log($"システムデータを保存しました: {systemDataPath}");
                }, cancellationToken: cts.Token);
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("SaveSystemDataAsyncがキャンセルされました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"SaveSystemDataAsyncでエラー: {ex.Message}");
            }
            finally
            {
                IsSaving = false;
                onComplete?.Invoke();
            }
        }

        public async UniTask LoadPlayerDataAsync(Action onComplete = null)
        {
            try
            {
                IsLoading = true;
                await UniTask.RunOnThreadPool(() =>
                {
                    if (File.Exists(playerDataPath))
                    {
                        byte[] encryptedData = File.ReadAllBytes(playerDataPath);
                        byte[] decryptedData = EncryptDecrypt(encryptedData);
                        PlayerProgress = DeserializeFromBinary<PlayerData>(decryptedData) ?? new PlayerData();
                        Debug.Log($"プレイヤーデータを読み込みました: {playerDataPath}");
                    }
                    else
                    {
                        PlayerProgress = new PlayerData();
                        SavePlayerDataAsync().Forget();
                        Debug.Log($"プレイヤーデータファイルが見つかりませんでした。新規作成: {playerDataPath}");
                    }
                }, cancellationToken: cts.Token);
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("LoadPlayerDataAsyncがキャンセルされました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"LoadPlayerDataAsyncでエラー: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                onComplete?.Invoke();
            }
        }

        public async UniTask SavePlayerDataAsync(Action onComplete = null)
        {
            try
            {
                IsSaving = true;
                await UniTask.RunOnThreadPool(() =>
                {
                    byte[] data = SerializeToBinary(PlayerProgress);
                    byte[] encryptedData = EncryptDecrypt(data);
                    File.WriteAllBytes(playerDataPath, encryptedData);
                    Debug.Log($"プレイヤーデータを保存しました: {playerDataPath}");
                }, cancellationToken: cts.Token);
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("SavePlayerDataAsyncがキャンセルされました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"SavePlayerDataAsyncでエラー: {ex.Message}");
            }
            finally
            {
                IsSaving = false;
                onComplete?.Invoke();
            }
        }

        public void Dispose()
        {
            cts?.Cancel();
            cts?.Dispose();
        }
    }
}
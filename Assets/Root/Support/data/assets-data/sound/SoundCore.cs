using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using AddressableSystem;
using GameCore.SaveSystem;
using GameCore.Enums;
namespace GameCore.Sound
{
    public class SoundCore : BaseSingleton<SoundCore>
    {
        private SoundDatabase database;
        private Dictionary<SoundGroup, Dictionary<SoundID, AddressableData<AudioClip>>> loadedClips =
            new Dictionary<SoundGroup, Dictionary<SoundID, AddressableData<AudioClip>>>();
        private AudioSource bgmSource;
        private AudioSource crossFadeTempSource;
        private List<AudioSource> sePool = new List<AudioSource>();
        private const int PoolSize = 30;
        private bool isLoadDatabase = false;
        public bool IsLoadDatabase => isLoadDatabase;

        private CancellationToken destroyToken;

        public void SetSystemBGMVolume()
        {
            if (bgmSource == null) return;
            if (!bgmSource.isPlaying) return;
            bgmSource.volume = SaveManagerCore.instance.SystemSettings.bgmVolume;
        }

        public void SetSystemSEVolume()
        {
            foreach(var clip in sePool)
            {
                clip.volume = SaveManagerCore.instance.SystemSettings.seVolume;
            }
        }

        public override void AwakeSingleton()
        {
            base.AwakeSingleton();
            instance = this;
            DontDestroyOnLoad(gameObject);

            // OnDestroy に紐づくキャンセルトークン
            destroyToken = this.GetCancellationTokenOnDestroy();

            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;

            for (int i = 0; i < PoolSize; i++)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                sePool.Add(source);
            }

            LoadDatabaseAsync().Forget();
        }

        private async UniTask LoadDatabaseAsync()
        {
            database = SoundBinaryReader.LoadSoundDatabaseFromBinary(SupportFiles.ALL_SOUND_BIN);
            if (database == null)
            {
                Debug.LogError("Failed to load SoundDatabase from binary.");
            }
            await UniTask.CompletedTask.AttachExternalCancellation(destroyToken);
            isLoadDatabase = true;
        }

        public void LoadGroup(SoundGroup group, GroupCategory groupCategory, Action action = null)
        {
            LoadGroupAsync(group, groupCategory, action).Forget();
        }

        public async UniTask LoadGroupAsync(SoundGroup group, GroupCategory groupCategory, Action action = null)
        {
            while (database == null)
            {
                await UniTask.Yield(cancellationToken: destroyToken);
            }
            if (loadedClips.ContainsKey(group)) return;
            var sounds = database.GroupedSoundsList.FirstOrDefault(data => data.Group == group);
            if (sounds == null) return;

            loadedClips[group] = new Dictionary<SoundID, AddressableData<AudioClip>>();
            var tasks = new List<UniTask>();

            foreach (var sound in sounds.Sounds)
            {
                var addressable = new AddressableData<AudioClip>(groupCategory, AssetCategory.Audio);
                
                tasks.Add(addressable.LoadAsync(sound.AddressablePath, clip =>
                {
                    if (addressable.IsLoadedAndSetup)
                    {
                        loadedClips[group][sound.SoundID] = addressable;
                    }
                }, ex =>
                {
                    Debug.LogError($"Failed to load audio clip for {sound.SoundID} at {sound.AddressablePath}: {ex.Message}");
                }).AttachExternalCancellation(destroyToken));
            }

            await UniTask.WhenAll(tasks);
            action?.Invoke();
        }

        public void UnloadGroup(SoundGroup group, GroupCategory groupCategory, Action action = null)
        {
            UnloadGroupAsync(group, groupCategory, action).Forget();
        }

        public async UniTask UnloadGroupAsync(SoundGroup group, GroupCategory groupCategory, Action action = null)
        {
            if (!loadedClips.TryGetValue(group, out var clips)) return;

            foreach (var addressable in clips.Values)
            {
                addressable.Release();
            }
            loadedClips.Remove(group);
            AddressableDataCore.Instance.ReleaseCategory(groupCategory, AssetCategory.Audio);
            action?.Invoke();
            await UniTask.CompletedTask.AttachExternalCancellation(destroyToken);
        }

        public void PlaySE(SoundGroup group, SoundID id, float volume = 1.0f, bool is3D = false, Vector3 position = default, float maxDistance = 500f)
        {
            PlaySEInternal(group, id, volume, is3D, position, maxDistance, async: true).Forget();
        }

        public async UniTask PlaySEAsync(SoundGroup group, SoundID id, float volume = 1.0f, bool is3D = false, Vector3 position = default, float maxDistance = 500f)
        {
            await PlaySEInternal(group, id, volume, is3D, position, maxDistance, async: true);
        }

        private async UniTask PlaySEInternal(SoundGroup group, SoundID id, float volume, bool is3D, Vector3 position, float maxDistance, bool async)
        {
            if (!TryGetClipAndData(group, id, out AddressableData<AudioClip> addressable, out SoundDatabase.SoundData data) || data.Type != SoundType.SE) return;

            var source = GetPooledSource();
            if (source == null) return;

            source.clip = addressable.GetAddressableObjectResult();
            source.volume = (data.BaseVolume * volume) * SaveManagerCore.instance.SystemSettings.seVolume;
            source.loop = false;
            source.spatialBlend = is3D ? 1f : 0f;
            source.maxDistance = maxDistance;
            if (is3D) source.transform.position = position;

            source.Play();

            if (async)
            {
                await UniTask.WaitUntil(() => !source.isPlaying, cancellationToken: destroyToken);
                ResetSource(source);
            }
        }

        public void PlayBGM(SoundGroup group, SoundID id, float volume = 1.0f, float fadeTime = 0f)
        {
            PlayBGMAsync(group, id, volume, fadeTime).Forget();
        }

        public async UniTask PlayBGMAsync(SoundGroup group, SoundID id, float volume = 1.0f, float fadeTime = 0f)
        {
            if (!TryGetClipAndData(group, id, out AddressableData<AudioClip> addressable, out SoundDatabase.SoundData data) || data.Type != SoundType.BGM) return;

            if (bgmSource.isPlaying && fadeTime > 0)
            {
                await FadeOutAsync(fadeTime);
            }

            bgmSource.clip = addressable.GetAddressableObjectResult();
            bgmSource.volume = 0f;
            bgmSource.Play();

            float fadeVolue = (data.BaseVolume * volume) * SaveManagerCore.instance.SystemSettings.bgmVolume;

            if (fadeTime > 0)
            {
                await FadeInAsync(fadeVolue, fadeTime);
            }
            else
            {
                bgmSource.volume = fadeVolue;
            }
        }

        public void FadeOutBGM(float fadeTime)
        {
            FadeOutAsync(fadeTime).Forget();
        }

        public async UniTask FadeOutAsync(float fadeTime, Action action = null)
        {
            if (!bgmSource.isPlaying)
            {
                action?.Invoke();
                return;
            }

            float startVolume = bgmSource.volume;
            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                bgmSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeTime);
                if (fadeTime >= timer)
                {
                    bgmSource.volume = 0.0f;
                    break;
                }
                await UniTask.Yield(cancellationToken: destroyToken);
            }
            bgmSource.Stop();
            ResetSource(bgmSource);
            action?.Invoke();
        }

        private async UniTask FadeInAsync(float targetVolume, float fadeTime, Action action = null)
        {
            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                bgmSource.volume = Mathf.Lerp(0f, targetVolume, timer / fadeTime);
                if (fadeTime >= timer)
                {
                    bgmSource.volume = targetVolume;
                    break;
                }
                await UniTask.Yield(cancellationToken: destroyToken);
            }
            action?.Invoke();
        }

        public void CrossFadeBGM(SoundGroup group, SoundID id, float volume = 1.0f, float fadeTime = 1f)
        {
            CrossFadeBGMAsync(group, id, volume, fadeTime).Forget();
        }

        public async UniTask CrossFadeBGMAsync(SoundGroup group, SoundID id, float volume = 1.0f, float fadeTime = 1f)
        {
            if (!TryGetClipAndData(group, id, out AddressableData<AudioClip> addressable, out SoundDatabase.SoundData data) || data.Type != SoundType.BGM) return;

            crossFadeTempSource = gameObject.AddComponent<AudioSource>();
            crossFadeTempSource.loop = true;
            crossFadeTempSource.clip = addressable.GetAddressableObjectResult();
            crossFadeTempSource.volume = 0f;
            crossFadeTempSource.Play();

            float timer = 0f;
            float startBGMVolume = bgmSource.volume;

            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                float t = timer / fadeTime;
                bgmSource.volume = Mathf.Lerp(startBGMVolume, 0f, t);
                crossFadeTempSource.volume = Mathf.Lerp(0f, (data.BaseVolume * volume) * SaveManagerCore.instance.SystemSettings.bgmVolume, t);
                await UniTask.Yield(cancellationToken: destroyToken);
            }

            bgmSource.Stop();
            ResetSource(bgmSource);
            Destroy(bgmSource);
            bgmSource = crossFadeTempSource;
            crossFadeTempSource = null;
        }

        private bool TryGetClipAndData(SoundGroup group, SoundID id, out AddressableData<AudioClip> addressable, out SoundDatabase.SoundData data)
        {
            addressable = null;
            data = null;
            if (!loadedClips.TryGetValue(group, out var groupClips) || !groupClips.TryGetValue(id, out addressable)) return false;
            data = database.GroupedSoundsList.FirstOrDefault(g => g.Group == group)?.Sounds.FirstOrDefault(s => s.SoundID == id);
            return data != null && addressable.IsLoadedAndSetup;
        }

        private AudioSource GetPooledSource()
        {
            return sePool.FirstOrDefault(s => !s.isPlaying && s.clip == null);
        }

        private void ResetSource(AudioSource source)
        {
            source.Stop();
            source.clip = null;
            source.volume = 0f;
            source.spatialBlend = 0f;
        }

        private void Update()
        {
            sePool.RemoveAll(s => s == null);
            if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnDestroy()
        {
            // destroyToken 経由ですべての UniTask がキャンセルされるので
            // ここでは loadedClips の解放などに専念できる
            foreach (var group in loadedClips.Values)
            {
                foreach (var clip in group.Values)
                {
                    clip.Release();
                }
            }
            loadedClips.Clear();
        }
    }
}

using System.Collections.Generic;

namespace GameCore.Sound {
    public class SoundDatabase {
        [System.Serializable]
        public class SoundData {
            private readonly string idName;
            private readonly string addressablePath;
            private readonly float baseVolume;
            private readonly SoundType type;
            private readonly SoundID soundID;
            public SoundData(SoundID soundID,string idName, string addressablePath, float baseVolume, SoundType type) {
                this.idName = idName;
                this.addressablePath = addressablePath;
                this.baseVolume = baseVolume;
                this.type = type;
                this.soundID = soundID;
            }
            public string IdName => idName;
            public string AddressablePath => addressablePath;
            public float BaseVolume => baseVolume;
            public SoundID SoundID => soundID;
            public SoundType Type => type;
        }
        [System.Serializable]
        public class GroupedSounds {
            private readonly SoundGroup group;
            private readonly List<SoundData> sounds;
            public GroupedSounds(SoundGroup group, List<SoundData> sounds) {
                this.group = group;
                this.sounds = sounds ?? new List<SoundData>();
            }
            public SoundGroup Group => group;
            public List<SoundData> Sounds => sounds;
        }
        private readonly List<GroupedSounds> groupedSounds;
        public SoundDatabase() {
            groupedSounds = new List<GroupedSounds>();
        }
        public List<GroupedSounds> GroupedSoundsList => groupedSounds;
    }
}

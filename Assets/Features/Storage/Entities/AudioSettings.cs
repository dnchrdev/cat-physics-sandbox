using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Storage.Entities
{
    public sealed class AudioSettings : IAudioSettings
    {
        public const int DefaultVolume = 50;

        public int Volume { get; private set; }
        public int MinVolume => 0;
        public int MaxVolume => 100;

        public AudioSettings()
        {
            ResetToDefaults();
        }

        public void SetVolume(int volume) => Volume = Mathf.Clamp(volume, MinVolume, MaxVolume);

        public void ResetToDefaults() => SetVolume(DefaultVolume);
    }
}

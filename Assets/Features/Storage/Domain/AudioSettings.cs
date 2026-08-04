using UnityEngine;

namespace Feature.Storage
{
    public class AudioSettings : IReadOnlyAudioSettings
    {
        public int Volume { get; private set; }

        public int MaxVolume => _maxVolume;

        public int MinVolume => _minVolume;

        private readonly int _minVolume;
        private readonly int _maxVolume;

        public AudioSettings(int volume = 50, int minVolume = 0, int maxVolume = 100)
        {
            _minVolume = Mathf.Max(minVolume, 0);
            _maxVolume = Mathf.Max(maxVolume, _minVolume);
            SetVolume(volume);
        }

        public void SetVolume(int newVolume)
        {
            Volume = Mathf.Clamp(newVolume, _minVolume, _maxVolume);
        }

    }
}

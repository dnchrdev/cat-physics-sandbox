namespace Features.Shared.Entities.Ports
{
    public interface IAudioSettings : IReadOnlyAudioSettings
    {
        void SetVolume(int volume);
    }
}

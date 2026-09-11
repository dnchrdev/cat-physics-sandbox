namespace Features.Storage.Domain.Interfaces
{
    public interface IReadOnlyAudioSettings
    {
        int Volume { get; }
        int MaxVolume { get; }
        int MinVolume { get; }
    }
}
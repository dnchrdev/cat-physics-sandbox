namespace Features.Shared.Entities.Ports
{
    public interface IReadOnlyAudioSettings
    {
        int Volume { get; }
        int MaxVolume { get; }
        int MinVolume { get; }
    }
}

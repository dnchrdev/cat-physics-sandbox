namespace Feature.Storage
{
    public interface IReadOnlyAudioSettings
    {
        int Volume { get; }
        int MaxVolume { get; }
        int MinVolume { get; }
    }
}
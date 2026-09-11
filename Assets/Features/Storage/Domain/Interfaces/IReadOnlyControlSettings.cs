namespace Features.Storage.Domain.Interfaces
{
    public interface IReadOnlyControlSettings
    {
        bool IsMobile { get; }
        int LookSensitivity { get; }
        int MinLookSensitivity { get; }
        int MaxLookSensitivity { get; }
    }
}
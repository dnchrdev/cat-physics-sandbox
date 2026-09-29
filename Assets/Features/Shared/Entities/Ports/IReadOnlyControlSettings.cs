namespace Features.Shared.Entities.Ports
{
    public interface IReadOnlyControlSettings
    {
        bool IsMobile { get; }
        int LookSensitivity { get; }
        int MinLookSensitivity { get; }
        int MaxLookSensitivity { get; }
    }
}

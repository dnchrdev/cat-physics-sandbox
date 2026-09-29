namespace Features.Shared.Entities.Ports
{

    public interface IControlSettings : IReadOnlyControlSettings
    {
        void SetLookSensitivity(int sensitivity);
        void SetIsMobile(bool isMobile);
    }
}

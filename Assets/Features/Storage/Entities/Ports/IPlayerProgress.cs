using Features.Shared.Entities.Ports;

namespace Features.Storage.Entities.Ports
{
    public interface IPlayerProgress : IReadOnlyPlayerProgress
    {
        void SetTutorialCompleted(bool value);
    }
}

using Features.Storage.Entities.Ports;

namespace Features.Storage.Entities
{
    public sealed class PlayerProgress : IPlayerProgress
    {
        public bool IsTutorialCompleted { get; private set; }

        public void SetTutorialCompleted(bool isTutorialCompleted) => IsTutorialCompleted = isTutorialCompleted;

        public void ResetToDefaults() => IsTutorialCompleted = false;
    }
}

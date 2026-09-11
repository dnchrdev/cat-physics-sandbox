using Features.Storage.Domain.Interfaces;

namespace Features.Storage.Domain
{
    public class PlayerProgress : IReadOnlyPlayerProgress
    {
        public bool IsTutorialCompleted { get; private set; }

        public PlayerProgress(bool isTutorialCompleted)
        {
            SetTutorialCompleted(isTutorialCompleted);
        }

        public void SetTutorialCompleted(bool isTutorialCompleted)
        {
            IsTutorialCompleted = isTutorialCompleted;
        }
    }
}
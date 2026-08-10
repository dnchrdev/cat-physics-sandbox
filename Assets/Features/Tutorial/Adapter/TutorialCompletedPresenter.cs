using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Feature.Tutorial
{
    public class TutorialCompletedPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly ITutorialCompletedView _view;
        [Inject] private readonly CompleteTutorialUseCase _completeTutorialUseCase;

        public void Initialize()
        {
            _view.StartGameRequestedEvent += HandleStartGameRequested;
        }

        public void Dispose()
        {
            _view.StartGameRequestedEvent -= HandleStartGameRequested;
        }

        private void HandleStartGameRequested()
        {
            _completeTutorialUseCase.StartGameplayAsync().Forget();
        }
    }
}
using System;
using Features.Tutorial.InterfaceAdapters.Ports;
using Features.Tutorial.UseCases;
using Zenject;

namespace Features.Tutorial.InterfaceAdapters
{
    public sealed class TutorialController : IInitializable, IDisposable, ITickable
    {
        private readonly TutorialCompletedChecker _completedChecker;
        private readonly StartGameplayUseCase _startGameplay;
        private readonly ITutorialCompletedView _completedView;

        public TutorialController(
            TutorialCompletedChecker completedChecker,
            StartGameplayUseCase startGameplay,
            ITutorialCompletedView completedView)
        {
            _completedChecker = completedChecker;
            _startGameplay = startGameplay;
            _completedView = completedView;
        }

        public void Initialize() => _completedView.StartGameRequestedEvent += _startGameplay.Execute;
        public void Dispose() => _completedView.StartGameRequestedEvent -= _startGameplay.Execute;
        public void Tick() => _completedChecker.Tick();
    }
}

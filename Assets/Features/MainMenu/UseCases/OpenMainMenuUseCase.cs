using Features.Core.UseCases;
using Features.MainMenu.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;

namespace Features.MainMenu.UseCases
{
    public sealed class OpenMainMenuUseCase
    {
        private readonly IReadOnlyPlayerProgress _playerProgress;
        private readonly ICursorController _cursor;
        private readonly GamePauseService _pause;
        private readonly IMainMenuOutputPort _output;

        public OpenMainMenuUseCase(
            IReadOnlyPlayerProgress playerProgress,
            ICursorController cursor,
            GamePauseService pause,
            IMainMenuOutputPort output)
        {
            _playerProgress = playerProgress;
            _cursor = cursor;
            _pause = pause;
            _output = output;
        }

        public void Execute()
        {
            _output.ShowStartGameAvailable(_playerProgress.IsTutorialCompleted);
            _output.ShowControlChoice(true);
            _cursor.Apply(true, CursorLockMode.Unlocked);
            _pause.Resume();
        }
    }
}

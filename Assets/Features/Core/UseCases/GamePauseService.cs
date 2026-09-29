using Features.Core.UseCases.Ports;

namespace Features.Core.UseCases
{
    public sealed class GamePauseService
    {
        private readonly ITimeProvider _timeProvider;

        public bool IsPaused { get; private set; }

        public GamePauseService(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void Pause()
        {
            if (IsPaused) return;
            IsPaused = true;
            _timeProvider.SetTimeScale(0f);
        }

        public void Resume()
        {
            if (IsPaused == false) return;
            IsPaused = false;
            _timeProvider.SetTimeScale(1f);
        }
    }
}

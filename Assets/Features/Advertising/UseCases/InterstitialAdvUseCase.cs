using Features.Advertising.Entities;
using Features.Advertising.UseCases.Ports;

namespace Features.Advertising.UseCases
{
    public class InterstitialAdvUseCase
    {
        private readonly InterstitialAdvTimer _timer;
        private readonly IGameplayStateGateway _gameplayState;
        private readonly IInterstitialAdvGateway _interstitialAdvGateway;
        private readonly IInterstitialAdvOutputPort _outputPort;

        public InterstitialAdvUseCase(
            InterstitialAdvTimer timer,
            IGameplayStateGateway gameplayState,
            IInterstitialAdvGateway interstitialAdvGateway,
            IInterstitialAdvOutputPort outputPort)
        {
            _timer = timer;
            _gameplayState = gameplayState;
            _interstitialAdvGateway = interstitialAdvGateway;
            _outputPort = outputPort;
        }

        public void Execute(float deltaTime)
        {
            if (_gameplayState.IsGameplayActive == false) return;

            InterstitialAdvTimer.Result result = _timer.Advance(deltaTime);

            if (result.ShouldShowCountdown)
            {
                _outputPort.PresentCountdownVisibility(true);
            }

            if (result.ShouldUpdateDisplayedSeconds)
            {
                _outputPort.PresentCountdownSeconds(result.DisplayedSeconds);
            }

            if (result.ShouldShowInterstitial)
            {
                _interstitialAdvGateway.ShowInterstitial();
            }

            if (result.ShouldHideCountdown)
            {
                _outputPort.PresentCountdownVisibility(false);
            }
        }
    }
}

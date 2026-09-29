namespace Features.Advertising.Entities
{
    public class InterstitialAdvTimer
    {
        private const int NO_DISPLAY_SECONDS = -1;

        private readonly float _intervalSeconds;
        private readonly float _countdownThresholdSeconds;

        private float _timeUntilAdv;
        private bool _isCountdownShown;
        private int _lastDisplayedSeconds = NO_DISPLAY_SECONDS;

        public InterstitialAdvTimer(float intervalSeconds, float countdownThresholdSeconds)
        {
            _intervalSeconds = intervalSeconds;
            _countdownThresholdSeconds = countdownThresholdSeconds;
            _timeUntilAdv = intervalSeconds;
        }

        public Result Advance(float deltaTime)
        {
            _timeUntilAdv -= deltaTime;

            bool shouldShowCountdown = false;
            bool shouldUpdateSeconds = false;
            int secondsToDisplay = _lastDisplayedSeconds;

            if (_timeUntilAdv <= _countdownThresholdSeconds)
            {
                if (_isCountdownShown == false)
                {
                    _isCountdownShown = true;
                    shouldShowCountdown = true;
                }

                int seconds = (int)_timeUntilAdv;
                if (seconds != _lastDisplayedSeconds)
                {
                    _lastDisplayedSeconds = seconds;
                    secondsToDisplay = seconds;
                    shouldUpdateSeconds = true;
                }
            }

            bool shouldShowInterstitial = false;
            bool shouldHideCountdown = false;

            if (_timeUntilAdv < 0)
            {
                shouldShowInterstitial = true;
                shouldHideCountdown = true;

                _timeUntilAdv = _intervalSeconds;
                _isCountdownShown = false;
                _lastDisplayedSeconds = NO_DISPLAY_SECONDS;
            }

            return new Result(
                shouldShowCountdown,
                shouldHideCountdown,
                shouldUpdateSeconds,
                secondsToDisplay,
                shouldShowInterstitial);
        }

        public readonly struct Result
        {
            public readonly bool ShouldShowCountdown;
            public readonly bool ShouldHideCountdown;
            public readonly bool ShouldUpdateDisplayedSeconds;
            public readonly int DisplayedSeconds;
            public readonly bool ShouldShowInterstitial;

            public Result(
                bool shouldShowCountdown,
                bool shouldHideCountdown,
                bool shouldUpdateDisplayedSeconds,
                int displayedSeconds,
                bool shouldShowInterstitial)
            {
                ShouldShowCountdown = shouldShowCountdown;
                ShouldHideCountdown = shouldHideCountdown;
                ShouldUpdateDisplayedSeconds = shouldUpdateDisplayedSeconds;
                DisplayedSeconds = displayedSeconds;
                ShouldShowInterstitial = shouldShowInterstitial;
            }
        }
    }
}

using Feature.Core;
using Feature.PlayerFeature;
using UnityEngine;
using Zenject;

namespace Feature.Advertising
{
    public class InterstitialAdvService : IInitializable, ITickable
    {
        private const float ADV_EVERY_SECONDS = 60f * 1f + 1f;
        private const float SHOW_ADV_COUNTDOWN = 10f + 1f;

        [Inject] private readonly IGamePauseService _gamePauseService;
        [Inject] private readonly IAdvRequestService _advRequestService;
        [Inject] private readonly IAdvView _view;
        [Inject] private readonly Player _player;

        private float _timerUntilAdv;
        private bool _panelShown;
        private int _lastDisplayedSeconds = -1;

        public void Initialize()
        {
            _view.ShowInterstitialPanel(false);
        }

        public void Tick()
        {
            if (_gamePauseService.Paused || _player.IsAlive == false) return;

            _timerUntilAdv -= Time.deltaTime;

            if (_timerUntilAdv <= SHOW_ADV_COUNTDOWN)
            {
                if (!_panelShown)
                {
                    _view.ShowInterstitialPanel(true);
                    _panelShown = true;
                }

                int seconds = (int)_timerUntilAdv;
                if (seconds != _lastDisplayedSeconds)
                {
                    _view.UpdateTimer(seconds);
                    _lastDisplayedSeconds = seconds;
                }
            }

            if (_timerUntilAdv < 0)
            {
                _advRequestService.ShowInterstitial();
                _timerUntilAdv = ADV_EVERY_SECONDS;
                _view.ShowInterstitialPanel(false);
                _panelShown = false;
                _lastDisplayedSeconds = -1;
            }
        }
    }
}
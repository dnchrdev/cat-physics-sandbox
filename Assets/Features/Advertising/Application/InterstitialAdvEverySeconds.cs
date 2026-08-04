using Feature.Core;
using Feature.PlayerFeature;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.Advertising
{
    public class InterstitialAdvEverySeconds : ITickable
    {
        private const float ADV_EVERY_SECONDS = 60f * 1f + 1f;
        private const float SHOW_ADV = 10f + 1f;

        private float _timerUntilAdv;
        private bool _panelShown;
        private int _lastDisplayedSeconds = -1;

        private IGamePauseService _gamePauseService;
        private IAdvRequestService _advRequestService;
        private AdvPresenter _presenter;
        private Player _player;

        [Inject]
        private void Construct(IGamePauseService gamePauseService, IAdvRequestService advRequestService, AdvPresenter advPresenter, Player player)
        {
            _gamePauseService = gamePauseService;
            _advRequestService = advRequestService;
            _presenter = advPresenter;
            _player = player;
            _presenter.ShowInterstitialPanel(false);
        }

        public void Tick()
        {
            if (_gamePauseService.Paused || _player.IsAlive == false) return;

            _timerUntilAdv -= Time.deltaTime;

            if (_timerUntilAdv <= SHOW_ADV)
            {
                if (!_panelShown)
                {
                    _presenter.ShowInterstitialPanel(true);
                    _panelShown = true;
                }

                int seconds = (int)_timerUntilAdv;
                if (seconds != _lastDisplayedSeconds)
                {
                    _presenter.UpdateTimer(seconds);
                    _lastDisplayedSeconds = seconds;
                }
            }

            if (_timerUntilAdv < 0)
            {
                _advRequestService.ShowInterstitial();
                _timerUntilAdv = ADV_EVERY_SECONDS;
                _presenter.ShowInterstitialPanel(false);
                _panelShown = false;
                _lastDisplayedSeconds = -1;
            }
        }
    }
}
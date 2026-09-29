using System;
using Features.Player.InterfaceAdapters.Ports;
using Features.Player.UseCases;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;
using Zenject;

namespace Features.Player.InterfaceAdapters.Presenters
{
    public sealed class KnockoutPresenter : IInitializable, IDisposable
    {
        private readonly PlayerRespawnUseCase _respawn;
        private readonly IRewardedAdvRequest _rewardedAdv;
        private readonly IPlayerLifeEvents _playerLifeEvents;
        private readonly IUIPanelsManager _panels;
        private readonly IKnockoutView _view;

        public KnockoutPresenter(
            PlayerRespawnUseCase respawn,
            IRewardedAdvRequest rewardedAdv,
            IPlayerLifeEvents playerLifeEvents,
            IUIPanelsManager panels,
            IKnockoutView view)
        {
            _respawn = respawn;
            _rewardedAdv = rewardedAdv;
            _playerLifeEvents = playerLifeEvents;
            _panels = panels;
            _view = view;
        }

        public void Initialize()
        {
            _view.RestartClickedEvent += OnRestartClicked;
            _view.ContinueClickedEvent += OnContinueClicked;
            _playerLifeEvents.Knockedout += OnKnockedOut;
        }

        public void Dispose()
        {
            _view.RestartClickedEvent -= OnRestartClicked;
            _view.ContinueClickedEvent -= OnContinueClicked;
            _playerLifeEvents.Knockedout -= OnKnockedOut;
        }

        private void OnKnockedOut() => _panels.OpenPanel(PanelMode.Knockout);

        private void OnRestartClicked()
        {
            _respawn.RespawnReset();
            _panels.OpenPanel(PanelMode.Gameplay);
        }

        private void OnContinueClicked() => _rewardedAdv.ShowRewarded(OnRewardedAdvCompleted);

        private void OnRewardedAdvCompleted()
        {
            _respawn.RespawnContinue();
            _panels.OpenPanel(PanelMode.Gameplay);
        }
    }
}

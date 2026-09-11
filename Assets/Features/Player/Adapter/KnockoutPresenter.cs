using System;
using Features.Advertising.Infrastructure.Interfaces;
using Features.Player.Adapter.Interfaces;
using Features.Player.Applications;
using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Data;
using Zenject;

namespace Features.Player.Adapter
{
    public class KnockoutPresenter : IInitializable, IDisposable
    {
        [Inject] private PlayerRespawnUseCase _respawnUseCase;
        [Inject] private IAdvRequestService _advRequestService;
        [Inject] private readonly Domain.Player _player;
        [Inject] private readonly UIPanelsManager _panelsManager;
        [Inject] private readonly IKnockoutView _knockoutView;
        
        public void Initialize()
        {
            _knockoutView.RestartClickedEvent += OnRestartCLicked;
            _knockoutView.ContinueClickedEvent += OnContinueClicked;
            _player.Knockouted += HandleKnockoutEvent;
        }

        public void Dispose()
        {
            _knockoutView.RestartClickedEvent -= OnRestartCLicked;
            _knockoutView.ContinueClickedEvent -= OnContinueClicked;
            _player.Knockouted -= HandleKnockoutEvent;
        }

        private void OnContinueClicked()
        {
            _advRequestService.RewardedAdvRequest(ContinueCallback);
        }

        private void ContinueCallback()
        {
            _respawnUseCase.RespawnContinue();
            _panelsManager.OpenPanel(PanelMode.Gameplay);
        }

        private void OnRestartCLicked()
        {
            _respawnUseCase.RespawnReset();
            _panelsManager.OpenPanel(PanelMode.Gameplay);
        }
        
        private void HandleKnockoutEvent() =>
            _panelsManager.OpenPanel(PanelMode.Knockout);
    }
}
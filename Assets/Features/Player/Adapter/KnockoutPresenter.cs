using System;
using System.Collections.Generic;
using Feature.Advertising;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class KnockoutPresenter : IInitializable, IDisposable
    {
        [Inject] private PlayerRespawnUseCase _respawnUseCase;
        [Inject] private IAdvRequestService _advRequestService;
        [Inject] private readonly Player _player;
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
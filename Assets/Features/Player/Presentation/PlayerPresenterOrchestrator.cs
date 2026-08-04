using System;
using Feature.Storage;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerPresenterOrchestrator : IInitializable, IDisposable
    {
        private IReadOnlyControlSettings _controlSettings;
        [Inject] private Player _player;
        private PCGameplayPresenter _pcGmaeplayPanel;
        private MobileControlPresenter _mobileGameplayControls;
        private PlayerKnockoutPanel _knockoutView;
        private UIPanelsManager _panelManager;
        [Inject] private PlayerGameStartedUseCase _playerGameStartedUseCase;

        [Inject]
        public PlayerPresenterOrchestrator(
            IReadOnlyControlSettings controlSettings,
            PCGameplayPresenter pcGmaeplayPanel,
            MobileControlPresenter mobileGameplayPanel,
            PlayerKnockoutPanel knockoutView,
            UIPanelsManager gameplayPanelCollection
        )
        {
            _controlSettings = controlSettings;
            _pcGmaeplayPanel = pcGmaeplayPanel;
            _mobileGameplayControls = mobileGameplayPanel;
            _knockoutView = knockoutView;
            _panelManager = gameplayPanelCollection;

            if (_controlSettings.IsMobile)
            {
                _panelManager.AddPanel(_mobileGameplayControls);
            }
            else
            {
                _panelManager.AddPanel(_pcGmaeplayPanel);
            }

            _mobileGameplayControls.InitPanel();
            _pcGmaeplayPanel.InitPanel();
            _panelManager.AddPanel(_knockoutView);
        }

        public void Initialize()
        {
            if (_controlSettings.IsMobile)
            {
                _mobileGameplayControls.OpenSettingsEvent += HandleOpenSettingsEvent;
                _mobileGameplayControls.OpenMobileAdjustmentEvent += OpenMobileAdjustmentEvent;
            }
            else
            {
                _pcGmaeplayPanel.OpenSettingsEvent += HandleOpenSettingsEvent;
            }

            // GameplayScene BOOTSTAP!
            _panelManager.OpenPanel(UIPanelTag.Gameplay);
            _playerGameStartedUseCase.GameStartedStart();

            _player.Knockouted += HandleKnockoutEvent;

            _knockoutView.OpenGameplay += HandleRespawn;
        }

        public void Dispose()
        {
            if (_controlSettings.IsMobile)
            {
                _panelManager.RemovePanel(_mobileGameplayControls);

                _mobileGameplayControls.OpenSettingsEvent -= HandleOpenSettingsEvent;
                _mobileGameplayControls.OpenMobileAdjustmentEvent -= OpenMobileAdjustmentEvent;
            }
            else
            {
                _panelManager.RemovePanel(_pcGmaeplayPanel);

                _pcGmaeplayPanel.OpenSettingsEvent -= HandleOpenSettingsEvent;
            }

            _player.Knockouted -= HandleKnockoutEvent;

            _panelManager.RemovePanel(_knockoutView);
            _knockoutView.OpenGameplay -= HandleRespawn;
        }

        private void HandleOpenSettingsEvent()
        {
            _panelManager.OpenPanel(UIPanelTag.Settings);
        }

        private void OpenMobileAdjustmentEvent()
        {
            _panelManager.OpenPanel(UIPanelTag.MobileButtonAdjustment);
        }

        private void HandleKnockoutEvent()
        {
            Debug.Log("Knockouted");
            _panelManager.OpenPanel(UIPanelTag.Knockout);
        }

        private void HandleRespawn()
        {
            _panelManager.OpenPanel(UIPanelTag.Gameplay);
        }
    }
}
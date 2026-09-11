using System;
using Features.Player.Adapter.Interfaces;
using Features.UI.Infrastructure.Data;
using Features.UI.Presentation.Buttons;
using Features.UI.Presentation.UIPanelManager.Interfaces;
using UnityEngine;

namespace Features.Player.Insrastructure
{
    public class KnockoutView: MonoBehaviour, IKnockoutView, IPanel
    {
        public event Action ContinueClickedEvent;
        public event Action RestartClickedEvent;
        
        [SerializeField] private ImageButton _continueButton;
        [SerializeField] private ImageButton _restartButton;

        public PanelMode[] PanelModes => new[] { PanelMode.Knockout };
        public PanelInput PanelInput => PanelInput.All;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            SubsribeButtons();
        }
        
        public void OnExitPanel()
        {
            UnsubsribeButtons();
            gameObject.SetActive(false);
        }
        
        private void SubsribeButtons()
        {
            _continueButton.Click += OnContinueButtonClicked;
            _restartButton.Click += OnRestartButtonClicked;
        }
        
        private void UnsubsribeButtons()
        {
            _continueButton.Click -= OnContinueButtonClicked;
            _restartButton.Click -= OnRestartButtonClicked;
        }

        private void OnContinueButtonClicked() => ContinueClickedEvent?.Invoke();
        private void OnRestartButtonClicked() => RestartClickedEvent?.Invoke();
    }
}
using System;
using Features.Player.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.UI.Infrastructure.Buttons;
using UnityEngine;

namespace Features.Player.Infrastructure.Views
{
    public class KnockoutView : MonoBehaviour, IKnockoutView, IPanel
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
            SubscribeButtons();
        }

        public void OnExitPanel()
        {
            UnsubscribeButtons();
            gameObject.SetActive(false);
        }

        private void SubscribeButtons()
        {
            _continueButton.Click += OnContinueButtonClicked;
            _restartButton.Click += OnRestartButtonClicked;
        }

        private void UnsubscribeButtons()
        {
            _continueButton.Click -= OnContinueButtonClicked;
            _restartButton.Click -= OnRestartButtonClicked;
        }

        private void OnContinueButtonClicked() => ContinueClickedEvent?.Invoke();
        private void OnRestartButtonClicked() => RestartClickedEvent?.Invoke();
    }
}

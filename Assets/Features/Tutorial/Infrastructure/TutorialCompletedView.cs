using System;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.Tutorial.InterfaceAdapters.Ports;
using Features.UI.Infrastructure.Buttons;
using UnityEngine;

namespace Features.Tutorial.Infrastructure
{
    public sealed class TutorialCompletedView : MonoBehaviour, ITutorialCompletedView, IPanel
    {
        private static readonly PanelMode[] Modes = { PanelMode.TutorialCompleted };

        public event Action StartGameRequestedEvent;

        public PanelMode[] PanelModes => Modes;
        public PanelInput PanelInput => PanelInput.All;

        [SerializeField] private ImageButton _startGameButton;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _startGameButton.Click += OnStartGameButtonClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _startGameButton.Click -= OnStartGameButtonClicked;
        }

        private void OnStartGameButtonClicked() => StartGameRequestedEvent?.Invoke();
    }
}

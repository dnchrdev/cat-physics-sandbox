using System;
using Features.MainMenu.InterfaceAdapters.Ports;
using Features.UI.Infrastructure.Buttons;
using UnityEngine;

namespace Features.MainMenu.Infrastructure
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        public event Action TutorialButtonClickedEvent;
        public event Action StartGameButtonClickedEvent;
        public event Action PCControlButtonClickedEvent;
        public event Action MobileControlButtonClickedEvent;

        [Header("Menu")]
        [SerializeField] private ImageButton _tutorialButton;
        [SerializeField] private ImageButton _startGameButton;

        [Header("Control")]
        [SerializeField] private GameObject _controlPanel;
        [SerializeField] private ImageButton _pcControlButton;
        [SerializeField] private ImageButton _mobileControlButton;

        private void Awake()
        {
            _tutorialButton.Click += OnTutorialButtonClicked;
            _startGameButton.Click += OnStartGameButtonClicked;
            _pcControlButton.Click += OnPCControlButtonClicked;
            _mobileControlButton.Click += OnMobileControlButtonClicked;
        }

        private void OnDestroy()
        {
            _tutorialButton.Click -= OnTutorialButtonClicked;
            _startGameButton.Click -= OnStartGameButtonClicked;
            _pcControlButton.Click -= OnPCControlButtonClicked;
            _mobileControlButton.Click -= OnMobileControlButtonClicked;
        }

        public void SetStartGameButtonEnabled(bool isEnabled)
        {
            if (isEnabled)
                _startGameButton.Enable();
            else
                _startGameButton.Disable();
        }

        public void ShowControlChoosePanel(bool isVisible)
        {
            _controlPanel.SetActive(isVisible);
        }

        private void OnTutorialButtonClicked() => TutorialButtonClickedEvent?.Invoke();
        private void OnStartGameButtonClicked() => StartGameButtonClickedEvent?.Invoke();
        private void OnPCControlButtonClicked() => PCControlButtonClickedEvent?.Invoke();
        private void OnMobileControlButtonClicked() => MobileControlButtonClickedEvent?.Invoke();
    }
}

using System;
using Feature.UI;
using UnityEngine;

namespace Feature.MainMenu
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

        public void SubscribeButtons()
        {
            _tutorialButton.Click += OnTutorialButtonClicked;
            _startGameButton.Click += OnStartGameButtonClicked;
            _pcControlButton.Click += OnPCControlButtonClicked;
            _mobileControlButton.Click += OnMobileControlButtonClicked;
        }

        public void UnsubscribeButtons()
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

        public void ShowControlChosePanel(bool isActive)
        {
            _controlPanel.gameObject.SetActive(isActive);
        }

        private void OnTutorialButtonClicked() => TutorialButtonClickedEvent?.Invoke();
        private void OnStartGameButtonClicked() => StartGameButtonClickedEvent?.Invoke();
        private void OnPCControlButtonClicked() => PCControlButtonClickedEvent?.Invoke();
        private void OnMobileControlButtonClicked() => MobileControlButtonClickedEvent?.Invoke();
    }
}
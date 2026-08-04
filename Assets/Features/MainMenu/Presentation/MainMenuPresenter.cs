using Cysharp.Threading.Tasks;
using Feature.Storage;
using Feature.UI;
using System;
using UnityEngine;
using Zenject;

namespace Feature.MainMenu
{
    public class MainMenuPresenter : MonoBehaviour, IInitializable, IDisposable
    {
        [Header("Menu")]
        [SerializeField] private ImageButton _tutorialButton;
        [SerializeField] private ImageButton _startGameButton;

        [Header("Control")]
        [SerializeField] private GameObject _controlPanel;
        [SerializeField] private ImageButton _pcControlButton;
        [SerializeField] private ImageButton _mobileControlButton;

        private StartGameUseCase _startGameUseCase;
        private ILoadingScreenService _loadingScreenService;
        private ControlSettings _controlSettings;
        private CursorManager _cursorManager;
        private IReadOnlyPlayerProgress _playerProgress;

        [Inject]
        public void Construct (StartGameUseCase startGameUseCase, UIAnimator animator, ControlSettings controlSettings, ILoadingScreenService loadingScreenService, CursorManager cursorManager, IReadOnlyPlayerProgress playerProgress)
        {
            _startGameUseCase = startGameUseCase;
            _controlSettings = controlSettings;
            _loadingScreenService = loadingScreenService;
            _cursorManager = cursorManager;
            _playerProgress = playerProgress;
        }

        public void Initialize()
        {
            _tutorialButton.Click += OnTutorialButtonClicked;

            Debug.Log($"playerProgress = {_playerProgress}");

            if (_playerProgress.IsTutorialCompleted)
            {
                _startGameButton.Enable();
                _startGameButton.Click += OnStartGameButtonClicked;
            }
            else
                _startGameButton.Disable();

            _pcControlButton.Click += OnPCControlButtonClicked;
            _mobileControlButton.Click += OnMobileControlButtonClicked;

            var menuState = new PanelState();
            menuState.LockState = CursorLockMode.None;
            menuState.Pause = false;
            menuState.CursorVisible = true;

            _cursorManager.ApplyState(menuState);

            ShowControlChosePanel(true);
        }

        public void Dispose()
        {
            _tutorialButton.Click -= OnTutorialButtonClicked;

            if (_playerProgress.IsTutorialCompleted)
            {
                _startGameButton.Click -= OnStartGameButtonClicked;
            }

            _pcControlButton.Click -= OnPCControlButtonClicked;
            _mobileControlButton.Click -= OnMobileControlButtonClicked;
        }

        private void ShowControlChosePanel(bool active)
        {
            _controlPanel.gameObject.SetActive(active);
        }

        private void OnTutorialButtonClicked()
        {
            _startGameUseCase.StartGame(isTutorial: true);
        }

        private void OnStartGameButtonClicked()
        {
            _startGameUseCase.StartGame(isTutorial: false);
        }

        private void OnPCControlButtonClicked()
        {
            _controlSettings.SetIsMobile(false);
            ShowControlChosePanel(false);
        }

        private void OnMobileControlButtonClicked()
        {
            _controlSettings.SetIsMobile(true);
            ShowControlChosePanel(false);
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
using Feature.Storage;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.MainMenu
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly IMainMenuView _view;
        [Inject] private readonly StartGameUseCase _startGameUseCase;
        [Inject] private readonly ControlSettings _controlSettings;
        [Inject] private readonly ILoadingScreenService _loadingScreenService;
        [Inject] private readonly CursorManager _cursorManager;
        [Inject] private readonly IReadOnlyPlayerProgress _playerProgress;

        public void Initialize()
        {
            _view.SubscribeButtons();

            _view.TutorialButtonClickedEvent += OnTutorialButtonClicked;
            _view.PCControlButtonClickedEvent += OnPCControlButtonClicked;
            _view.MobileControlButtonClickedEvent += OnMobileControlButtonClicked;

            if (_playerProgress.IsTutorialCompleted)
            {
                _view.SetStartGameButtonEnabled(true);
                _view.StartGameButtonClickedEvent += OnStartGameButtonClicked;
            }
            else
            {
                _view.SetStartGameButtonEnabled(false);
            }

            var menuState = new PanelState
            {
                LockState = CursorLockMode.None,
                IsPaused = false,
                IsCursorVisible = true
            };

            _cursorManager.ApplyState(menuState);

            _view.ShowControlChosePanel(true);
        }

        public void Dispose()
        {
            _view.UnsubscribeButtons();

            _view.TutorialButtonClickedEvent -= OnTutorialButtonClicked;
            _view.PCControlButtonClickedEvent -= OnPCControlButtonClicked;
            _view.MobileControlButtonClickedEvent -= OnMobileControlButtonClicked;

            if (_playerProgress.IsTutorialCompleted)
            {
                _view.StartGameButtonClickedEvent -= OnStartGameButtonClicked;
            }
        }

        private void OnTutorialButtonClicked()
        {
            OnTutorialButtonClickedAsync().Forget();
        }

        private async UniTask OnTutorialButtonClickedAsync()
        {
            var result = await _startGameUseCase.StartTutorialAsync();
            if (result.IsSuccess == false)
            {
                Debug.LogError(result.Message);
            }
        }

        private void OnStartGameButtonClicked()
        {
            OnStartGameButtonClickedAsync().Forget();
        }

        private async UniTask OnStartGameButtonClickedAsync()
        {
            var result = await _startGameUseCase.StartGameplayAsync();
            if (result.IsSuccess == false)
            {
                Debug.LogError(result.Message);
            }
        }

        private void OnPCControlButtonClicked()
        {
            _controlSettings.SetIsMobile(false);
            _view.ShowControlChosePanel(false);
        }

        private void OnMobileControlButtonClicked()
        {
            _controlSettings.SetIsMobile(true);
            _view.ShowControlChosePanel(false);
        }
    }
}
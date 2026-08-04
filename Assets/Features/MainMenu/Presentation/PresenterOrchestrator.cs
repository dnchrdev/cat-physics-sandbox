using Cysharp.Threading.Tasks;
using Feature.Scene;
using Feature.Storage;
using Feature.UI;
using System;
using UnityEngine;
using Zenject;

namespace Feature.MainMenu
{
    public class PresenterOrchestrator /*: IInitializable, IDisposable*/
    {
        //private IStartGameUseCase _startGameUseCase;
        //private MainMenuPresenter _mainMenuPresenter;
        //private ILoadingScreenService _loadingScreenService;
        //private ControlSettings _controlSettings;
        //private CursorManager _cursorManager;


        //public PresenterOrchestrator(IStartGameUseCase startGameUseCase, MainMenuPresenter view, UIAnimator animator, ControlSettings controlSettings, ILoadingScreenService loadingScreenService, CursorManager cursorManager)
        //{
        //    _startGameUseCase = startGameUseCase;
        //    _mainMenuPresenter = view;
        //    _controlSettings = controlSettings;
        //    _loadingScreenService = loadingScreenService;
        //    _cursorManager = cursorManager;
        //}

        //public void Initialize()
        //{
        //    _mainMenuPresenter.StartGameButtonClicked += OnStartGameButtonClicked;
        //    _mainMenuPresenter.IsMobileControlChosedEvent += OnIsMobileControlChosed;

        //    _mainMenuPresenter.ShowControlChosePanel(true);

        //    var menuState = new PanelState();
        //    menuState.LockState = CursorLockMode.None;
        //    menuState.Pause = false;
        //    menuState.CursorVisible = true;

        //    _cursorManager.ApplyState(menuState);
        //}

        //public void Dispose()
        //{
        //    _mainMenuPresenter.StartGameButtonClicked -= OnStartGameButtonClicked;
        //    _mainMenuPresenter.IsMobileControlChosedEvent -= OnIsMobileControlChosed;
        //}

        //private void OnStartGameButtonClicked()
        //{
        //    StartGameButtonClickedAsync().Forget();
        //}

        //private void OnIsMobileControlChosed(bool isMobile)
        //{
        //    _controlSettings.SetIsMobile(isMobile);
        //    _mainMenuPresenter.ShowControlChosePanel(false);
        //}

        //private async UniTask StartGameButtonClickedAsync()
        //{
        //    await _loadingScreenService.StartLoadingAsync();
        //    await _startGameUseCase.StartGameplayAsync();
        //    await _loadingScreenService.EndLoadingAsync();
        //}


    }
}

using System;
using Features.MainMenu.InterfaceAdapters.Ports;
using Features.MainMenu.UseCases;
using Zenject;

namespace Features.MainMenu.InterfaceAdapters.Controllers
{
    public sealed class MainMenuController : IInitializable, IDisposable
    {
        private readonly IMainMenuView _view;
        private readonly OpenMainMenuUseCase _openMenu;
        private readonly StartGameUseCase _startGame;
        private readonly ChooseControlUseCase _chooseControl;

        public MainMenuController(
            IMainMenuView view,
            OpenMainMenuUseCase openMenu,
            StartGameUseCase startGame,
            ChooseControlUseCase chooseControl)
        {
            _view = view;
            _openMenu = openMenu;
            _startGame = startGame;
            _chooseControl = chooseControl;
        }

        public void Initialize()
        {
            _view.TutorialButtonClickedEvent += _startGame.StartTutorial;
            _view.StartGameButtonClickedEvent += _startGame.StartGameplay;
            _view.PCControlButtonClickedEvent += _chooseControl.ChoosePC;
            _view.MobileControlButtonClickedEvent += _chooseControl.ChooseMobile;

            _openMenu.Execute();
        }

        public void Dispose()
        {
            _view.TutorialButtonClickedEvent -= _startGame.StartTutorial;
            _view.StartGameButtonClickedEvent -= _startGame.StartGameplay;
            _view.PCControlButtonClickedEvent -= _chooseControl.ChoosePC;
            _view.MobileControlButtonClickedEvent -= _chooseControl.ChooseMobile;
        }
    }
}

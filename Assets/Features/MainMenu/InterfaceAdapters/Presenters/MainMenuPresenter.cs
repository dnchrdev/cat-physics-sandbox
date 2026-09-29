using Features.MainMenu.InterfaceAdapters.Ports;
using Features.MainMenu.UseCases.Ports;

namespace Features.MainMenu.InterfaceAdapters.Presenters
{
    public sealed class MainMenuPresenter : IMainMenuOutputPort
    {
        private readonly IMainMenuView _view;

        public MainMenuPresenter(IMainMenuView view)
        {
            _view = view;
        }

        public void ShowStartGameAvailable(bool isAvailable) => _view.SetStartGameButtonEnabled(isAvailable);
        public void ShowControlChoice(bool isVisible) => _view.ShowControlChoosePanel(isVisible);
    }
}

using System;

namespace Features.MainMenu.InterfaceAdapters.Ports
{
    public interface IMainMenuView
    {
        event Action TutorialButtonClickedEvent;
        event Action StartGameButtonClickedEvent;
        event Action PCControlButtonClickedEvent;
        event Action MobileControlButtonClickedEvent;

        void SetStartGameButtonEnabled(bool isEnabled);
        void ShowControlChoosePanel(bool isVisible);
    }
}

using System;
using Feature.UI;

namespace Feature.MainMenu
{
    public interface IMainMenuView
    {
        event Action TutorialButtonClickedEvent;
        event Action StartGameButtonClickedEvent;
        event Action PCControlButtonClickedEvent;
        event Action MobileControlButtonClickedEvent;

        void SubscribeButtons();
        void UnsubscribeButtons();

        void SetStartGameButtonEnabled(bool isEnabled);
        void ShowControlChosePanel(bool isActive);
    }
}
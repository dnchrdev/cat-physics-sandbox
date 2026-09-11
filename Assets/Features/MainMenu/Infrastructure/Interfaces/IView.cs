using System;

namespace Features.MainMenu.Infrastructure.Interfaces
{
    public interface IView
    {
        event Action StartGameButtonClicked;
        event Action<bool> IsMobileControlChosedEvent;
        void ShowControlChosePanel(bool active);
    }
}
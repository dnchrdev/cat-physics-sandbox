using System;
using Feature.UI;

namespace Feature.Tutorial
{
    public interface ITutorialCompletedView
    {
        event Action StartGameRequestedEvent;

        void SetActive(bool isActive);
    }
}
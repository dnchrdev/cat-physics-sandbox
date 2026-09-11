using System;

namespace Features.Tutorial.Adapter.Interfaces
{
    public interface ITutorialCompletedView
    {
        event Action StartGameRequestedEvent;

        void SetActive(bool isActive);
    }
}
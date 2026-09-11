using System;

namespace Features.Player.Adapter.Interfaces
{
    public interface IKnockoutView
    {
        event Action ContinueClickedEvent;
        event Action RestartClickedEvent;
    }
}
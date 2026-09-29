using System;

namespace Features.Player.InterfaceAdapters.Ports
{
    public interface IKnockoutView
    {
        event Action ContinueClickedEvent;
        event Action RestartClickedEvent;
    }
}

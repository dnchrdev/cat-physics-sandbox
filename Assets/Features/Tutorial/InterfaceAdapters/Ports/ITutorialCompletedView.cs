using System;

namespace Features.Tutorial.InterfaceAdapters.Ports
{
    public interface ITutorialCompletedView
    {
        event Action StartGameRequestedEvent;
   }
}

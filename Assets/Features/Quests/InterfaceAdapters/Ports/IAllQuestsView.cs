using System;

namespace Features.Quests.InterfaceAdapters.Ports
{
    public interface IAllQuestsView
    {
        event Action CloseRequested;
        event Action PanelEntered;
        event Action PanelExited;
    }
}

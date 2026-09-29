using System;

namespace Features.Player.InterfaceAdapters.Ports
{
    public interface IGameplayControlView
    {
        event Action SettingsOpenedEvent;
    }
}

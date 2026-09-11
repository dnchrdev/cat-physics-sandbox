using System;

namespace Features.Player.Adapter.Interfaces
{
    public interface IGameplayControlView
    {
        event Action SettingsOpenedEvent;
    }
}
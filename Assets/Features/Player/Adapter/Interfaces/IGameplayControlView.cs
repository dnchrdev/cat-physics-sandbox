using System;
using Feature.UI;

namespace Feature.PlayerFeature
{
    public interface IGameplayControlView
    {
        event Action SettingsOpenedEvent;
    }
}
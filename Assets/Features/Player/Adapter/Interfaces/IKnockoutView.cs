using System;
using Feature.UI;

namespace Feature.PlayerFeature
{
    public interface IKnockoutView
    {
        event Action ContinueClickedEvent;
        event Action RestartClickedEvent;
    }
}
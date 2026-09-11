using System;
using UnityEngine;

namespace Features.Quests.Adapter.Interfaces
{
    public interface IAllQuestsView
    {
        event Action CloseRequestedEvent;
        event Action PanelEnteredEvent;
        event Action PanelExitedEvent;

        void SetActive(bool isActive);
        Transform GetShowedContentParent();
        Transform GetHiddenContentParent();
    }
}
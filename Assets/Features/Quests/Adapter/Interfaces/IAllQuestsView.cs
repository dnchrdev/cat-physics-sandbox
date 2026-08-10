using System;
using Feature.UI;
using UnityEngine;

namespace Feature.Quests
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
using System;
using System.Collections;
using UnityEngine;

namespace Feature.Input
{
    public interface IUIPanelInput
    {
        event Action ToggleSettingsEvent;
        event Action AllQuestsEvent;
        event Action QuestHintsEvent;

        void ToggleSettings();
        void AllQuests();
        void QuestTip();
    }
}
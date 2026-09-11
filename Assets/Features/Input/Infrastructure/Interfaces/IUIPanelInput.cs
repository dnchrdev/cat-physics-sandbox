using System;

namespace Features.Input.Infrastructure.Interfaces
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
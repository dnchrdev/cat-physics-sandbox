using System;

namespace Features.Shared.UseCases.Input
{
    public interface IUIPanelInput
    {
        event Action ToggleSettingsEvent;
        event Action AllQuestsEvent;
        event Action QuestHintRequestedEvent;
        void ToggleSettings();
        void AllQuests();
        void RequestQuestHint();
    }
}
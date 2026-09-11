using System;

namespace Features.Quests.Adapter.Interfaces
{
    public interface ICurrentQuestView
    {
        event Action HintsRequestedEvent;
        event Action AllQuestsRequestedEvent;

        void SetActive(bool isActive);
        void SetDescription(string description);
        void SetProgress(float ratio);
    }
}
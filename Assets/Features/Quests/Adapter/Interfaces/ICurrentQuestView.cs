using System;
using Feature.UI;

namespace Feature.Quests
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
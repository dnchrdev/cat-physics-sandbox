using System;

namespace Features.Quests.InterfaceAdapters.Ports
{
    public interface ICurrentQuestView
    {
        event Action HintsRequested;
        event Action AllQuestsRequested;

        void SetDescription(string description);
        void SetProgress(float progress);
    }
}

using System;
using System.Collections.Generic;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.InterfaceAdapters.Ports
{
    public interface IQuestCardsView
    {
        event Action<string> QuestSelected;

        void ShowCards(IReadOnlyList<QuestSummary> quests);
        void HideCards();
    }
}

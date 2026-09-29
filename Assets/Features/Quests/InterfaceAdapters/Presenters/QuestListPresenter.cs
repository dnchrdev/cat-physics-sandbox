using System.Collections.Generic;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.InterfaceAdapters.Presenters
{
    public sealed class QuestListPresenter : IQuestListOutput
    {
        private readonly IQuestCardsView _cards;

        public QuestListPresenter(IQuestCardsView cards)
        {
            _cards = cards;
        }

        public void ShowQuestList(IReadOnlyList<QuestSummary> quests) => _cards.ShowCards(quests);
        public void HideQuestList() => _cards.HideCards();
    }
}

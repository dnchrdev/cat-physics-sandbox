using System.Linq;
using Features.Quests.Entities;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.UseCases
{
    public sealed class QuestListUseCase
    {
        private readonly QuestsCollection _quests;
        private readonly IQuestListOutput _output;

        public QuestListUseCase(QuestsCollection quests, IQuestListOutput output)
        {
            _quests = quests;
            _output = output;
        }

        public void Show() =>
            _output.ShowQuestList(_quests.All
                .Select(quest => new QuestSummary(quest.Name, quest.Description, quest.ProgressRatio))
                .ToList());

        public void Hide() => _output.HideQuestList();
    }
}

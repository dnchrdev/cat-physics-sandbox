using Features.Quests.Entities;

namespace Features.Quests.UseCases
{
    public sealed class RecordQuestProgressUseCase
    {
        private readonly QuestsCollection _quests;
        private readonly CurrentQuestReporter _reporter;

        public RecordQuestProgressUseCase(QuestsCollection quests, CurrentQuestReporter reporter)
        {
            _quests = quests;
            _reporter = reporter;
        }

        public void Execute(string questName, QuestTargetEvent targetEvent)
        {
            if (!_quests.TryGet(questName, out var quest) || !quest.Record(targetEvent))
                return;

            if (_quests.IsCurrent(quest))
                _reporter.Report();
        }
    }
}

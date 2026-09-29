using Features.Quests.Entities;

namespace Features.Quests.UseCases
{
    public sealed class ResetAllQuestsUseCase
    {
        private readonly QuestsCollection _quests;
        private readonly CurrentQuestReporter _reporter;

        public ResetAllQuestsUseCase(QuestsCollection quests, CurrentQuestReporter reporter)
        {
            _quests = quests;
            _reporter = reporter;
        }

        public void Execute()
        {
            _quests.ResetAll();
            _reporter.Report();
        }
    }
}

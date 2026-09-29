using Features.Quests.Entities;
using Features.Shared.Entities;
using Features.Shared.UseCases;

namespace Features.Quests.UseCases
{
    public sealed class SelectQuestUseCase
    {
        private readonly QuestsCollection _quests;
        private readonly CurrentQuestReporter _reporter;
        private readonly IUIPanelsManager _panels;

        public SelectQuestUseCase(QuestsCollection quests, CurrentQuestReporter reporter, IUIPanelsManager panels)
        {
            _quests = quests;
            _reporter = reporter;
            _panels = panels;
        }

        public void Execute(string questName)
        {
            _quests.Select(questName);
            _reporter.Report();
            _panels.OpenPanel(PanelMode.Gameplay);
        }
    }
}

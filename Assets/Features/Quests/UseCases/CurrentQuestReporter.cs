using System.Linq;
using Features.Quests.Entities;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.UseCases
{
    public sealed class CurrentQuestReporter
    {
        private readonly QuestsCollection _quests;
        private readonly ICurrentQuestOutput _currentQuest;
        private readonly IQuestHintsOutput _hints;

        public CurrentQuestReporter(QuestsCollection quests, ICurrentQuestOutput currentQuest, IQuestHintsOutput hints)
        {
            _quests = quests;
            _currentQuest = currentQuest;
            _hints = hints;
        }

        public void Report()
        {
            var current = _quests.Current;
            if (current == null)
                return;

            _currentQuest.ShowCurrentQuest(current.Description, current.ProgressRatio);

            if (current.IsHintsVisible)
                _hints.ShowHints(current.Name, current.RemainingTargets().ToList());
            else
                _hints.HideHints();
        }
    }
}

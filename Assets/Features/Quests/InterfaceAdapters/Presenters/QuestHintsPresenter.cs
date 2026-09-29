using System.Collections.Generic;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.InterfaceAdapters.Presenters
{
    public sealed class QuestHintsPresenter : IQuestHintsOutput
    {
        private readonly Dictionary<string, IQuestSource> _sources = new();
        private readonly IQuestHintsView _view;

        public QuestHintsPresenter(List<IQuestSource> sources, IQuestHintsView view)
        {
            foreach (var source in sources)
                _sources[source.QuestName] = source;

            _view = view;
        }

        public void ShowHints(string questName, IReadOnlyList<int> targetIndices)
        {
            if (!_sources.TryGetValue(questName, out var source))
            {
                _view.Hide();
                return;
            }

            var targets = new List<IHintAnchor>(targetIndices.Count);
            foreach (var index in targetIndices)
                targets.Add(source.GetTargetAnchor(index));

            _view.Show(targets, source.TipAnchors);
        }

        public void HideHints() => _view.Hide();
    }
}

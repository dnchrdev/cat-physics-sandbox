using System.Collections.Generic;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.InterfaceAdapters.Presenters
{
    public sealed class CurrentQuestPresenter : ICurrentQuestOutput
    {
        private readonly IReadOnlyList<ICurrentQuestView> _views;

        public CurrentQuestPresenter(List<ICurrentQuestView> views)
        {
            _views = views;
        }

        public void ShowCurrentQuest(string description, float progress)
        {
            foreach (var view in _views)
            {
                view.SetDescription(description);
                view.SetProgress(progress);
            }
        }
    }
}

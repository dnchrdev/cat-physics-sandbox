using System;
using System.Collections.Generic;
using Feature.Advertising;
using Feature.UI;
using Zenject;

namespace Feature.Quests
{
    public class QuestHintsPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly QuestTipsManager _questHintsManager;
        [Inject] private readonly IAdvRequestService _advRequestService;
        [Inject] private readonly IQuestHintsView _view;
        [Inject] private readonly QuestsCollection _questsCollection;

        public void Initialize()
        {
            _questsCollection.ShowHintsEvent += ShowQuestHints;
            _questsCollection.CurrentQuestUpdated += ShowQuestHints;
        }

        public void Dispose()
        {
            _questsCollection.ShowHintsEvent -= ShowQuestHints;
            _questsCollection.CurrentQuestUpdated -= ShowQuestHints;
        }

        private void ShowQuestHints(Quest quest)
        {
            _questHintsManager.HideTips(_view.GetHiddenTipParent());

            if (quest.IsHintsVisible == false) return;

            _questHintsManager.ShowHints(quest, _view.GetVisibleTipParent());
        }
    }
}
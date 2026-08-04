using System;
using System.Collections.Generic;
using Feature.Advertising;
using Feature.UI;
using Zenject;

namespace Feature.Quests
{
    public class QuestHintsPresenter : IPanel, IInitializable, IDisposable
    {
        [Inject] private readonly QuestTipsManager _questHintsManager;
        [Inject] private readonly IAdvRequestService  _advRequestService;
        private readonly QuestHitsView _questHitsView;
        private readonly UIPanelsManager _panelsManager;
        private readonly QuestsCollection _questsCollection;

        public List<UIPanelTag> PanelTags => _tags;

        private List<UIPanelTag> _tags = new List<UIPanelTag>()
        {
            UIPanelTag.Gameplay
        };

        public QuestHintsPresenter(UIPanelsManager panelsManager, QuestsCollection questsCollection, QuestHitsView questHitsView)
        {
            _questHitsView = questHitsView;
            _panelsManager = panelsManager;
            _questsCollection = questsCollection;

            _panelsManager.AddPanel(this);

            _questsCollection.ShowHintsEvent += ShowQuestHints;
            _questsCollection.CurrentQuestUpdated += ShowQuestHints;
        }

        private void ShowQuestHints(Quest quest)
        {
            _questHintsManager.HideTips(_questHitsView.HiddenTipParent);

            if (quest.IsHintsVisible == false) return;
            _questHintsManager.ShowHints(quest, _questHitsView.VisibleTipParent);
        }

        public void Dispose()
        {
            _panelsManager.RemovePanel(this);

            _questsCollection.ShowHintsEvent -= ShowQuestHints;
            _questsCollection.CurrentQuestUpdated -= ShowQuestHints;
        }

        public void InitPanel()
        {
            _questHitsView.gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            _questHitsView.gameObject.SetActive(true);
        }

        public void OnExitPanel()
        {
            _questHitsView.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            
        }
    }
}
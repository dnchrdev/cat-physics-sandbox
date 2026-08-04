using Feature.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Quests
{
    public class AllQuestsPresenter: IPanel, IDisposable
    {
        private QuestsCollection _questsCollection;
        private AllQuestsView _allQuestsView;
        private UIPanelsManager _panelsManager;
        private QuestCaldsManager _questCaldsManager;

        public List<UIPanelTag> PanelTags => _tags;
        private List<UIPanelTag> _tags = new List<UIPanelTag>() 
        { 
            UIPanelTag.AllQuests 
        };

        public AllQuestsPresenter(QuestsCollection questsCollection, AllQuestsView allQuestsView, UIPanelsManager panelsManager, QuestCaldsManager questCaldsManager)
        {
            _questsCollection = questsCollection;
            _allQuestsView = allQuestsView;
            _panelsManager = panelsManager;
            _questCaldsManager = questCaldsManager;

            _panelsManager.AddPanel(this);
        }

        public void Dispose()
        {
            _panelsManager.RemovePanel(this);
        }

        public void InitPanel()
        {
            _allQuestsView.gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            _allQuestsView.gameObject.SetActive(true);

            _allQuestsView.ClosedButton.Click += ClosePanel;
            _questCaldsManager.CreateAllQuestCards(_allQuestsView.ShowedContent);
            _questCaldsManager.NewQuestSelected += HandleNewQuestSelected;
        }
        public void OnExitPanel()
        {
            _allQuestsView.gameObject.SetActive(false);
            _questCaldsManager.HideAllQuestCards(_allQuestsView.HiddenContent);
            _questCaldsManager.NewQuestSelected -= HandleNewQuestSelected;
        }

        private void HandleNewQuestSelected(string name)
        {
            _questsCollection.SwitchCurrentQuest(name);
            ClosePanel();
        }

        private void ClosePanel()
        {
            _panelsManager.OpenPanel(UIPanelTag.Gameplay);
        }


    }
}
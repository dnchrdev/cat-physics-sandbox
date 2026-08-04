using System;
using System.Collections.Generic;
using Feature.Advertising;
using Feature.Input;
using Feature.PlayerFeature;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.Quests
{
    public class CurrentQuestPresenter : IInitializable, IDisposable, IPanel
    {
        [Inject] private readonly Player _player;
        [Inject] private IUIPanelInput _input;
        [Inject] private readonly IAdvRequestService _advRequestService;
        [Inject] private readonly ResetAllQuestsUseCase  _resetAllQuestsUseCase;

        private readonly CurrentQuestView _currentQuestView;
        private readonly UIPanelsManager _panelsManager;
        private readonly QuestsCollection _questsCollection;

        public List<UIPanelTag> PanelTags => Tags;
        private List<UIPanelTag> Tags = new List<UIPanelTag>()
        {
            UIPanelTag.Gameplay
        };
        
        public CurrentQuestPresenter(
            CurrentQuestView currentQuestView, 
            UIPanelsManager panelsManager,
            QuestsCollection questsCollection)
        {
            _currentQuestView = currentQuestView;
            _panelsManager = panelsManager;
            _questsCollection = questsCollection;
            
            _questsCollection.CurrentQuestUpdated += OnCurrentQuestUpdated;
            _panelsManager.AddPanel(this);
        }

        public void InitPanel()
        {
            _currentQuestView.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _player.Respawned += _resetAllQuestsUseCase.ResetAllQuests;
        }
        
        public void Dispose()
        {
            _questsCollection.CurrentQuestUpdated -= OnCurrentQuestUpdated;
            _player.Respawned -= _resetAllQuestsUseCase.ResetAllQuests;
        }

        public void OnEnterPanel()
        {
            _currentQuestView.gameObject.SetActive(true);
            
            _input.AllQuestsEvent += ShowAllQuests;
            _input.QuestHintsEvent += ShowHints;
            _currentQuestView.HintsButton.Click += ShowHints;
            _currentQuestView.AllQuestsButton.Click += ShowAllQuests;
        }
        
        public void OnExitPanel()
        {
            _currentQuestView.gameObject.SetActive(false);

            _input.AllQuestsEvent -= ShowAllQuests;
            _input.QuestHintsEvent -= ShowHints;
            _currentQuestView.HintsButton.Click -= ShowHints;
            _currentQuestView.AllQuestsButton.Click -= ShowAllQuests;
        }
        
        private void OnCurrentQuestUpdated(Quest quest)
        {
            _currentQuestView.Description.text = quest.Description;
            _currentQuestView.FillImage.fillAmount = quest.ProgressRatio;
        }

        private void ShowHints()
        {
            if (_questsCollection.Current.IsCompleted || _questsCollection.Current.IsHintsVisible) return;

            Action showHintsCallback = () => { _questsCollection.ShowHints(_questsCollection.Current.Name); };

            _advRequestService.RewardedAdvRequest(showHintsCallback);
        }

        private void ShowAllQuests()
        {
            _panelsManager.OpenPanel(UIPanelTag.AllQuests);
        }
    }
}
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
    public class CurrentQuestPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly IAdvRequestService _advRequestService;
        [Inject] private readonly ResetAllQuestsUseCase _resetAllQuestsUseCase;
        [Inject] private readonly ICurrentQuestView _view;
        [Inject] private readonly UIPanelsManager _panelsManager;
        [Inject] private readonly QuestsCollection _questsCollection;

        public void Initialize()
        {
            _questsCollection.CurrentQuestUpdated += OnCurrentQuestUpdated;
            _player.Respawned += _resetAllQuestsUseCase.ResetAllQuests;

            _view.HintsRequestedEvent += ShowHints;
            _view.AllQuestsRequestedEvent += ShowAllQuests;
        }

        public void Dispose()
        {
            _questsCollection.CurrentQuestUpdated -= OnCurrentQuestUpdated;
            _player.Respawned -= _resetAllQuestsUseCase.ResetAllQuests;

            _view.HintsRequestedEvent -= ShowHints;
            _view.AllQuestsRequestedEvent -= ShowAllQuests;
        }

        private void OnCurrentQuestUpdated(Quest quest)
        {
            _view.SetDescription(quest.Description);
            _view.SetProgress(quest.ProgressRatio);
        }

        private void ShowHints()
        {
            if (_questsCollection.Current.IsCompleted || _questsCollection.Current.IsHintsVisible) return;

            Action showHintsCallback = () => { _questsCollection.ShowHints(_questsCollection.Current.Name); };

            _advRequestService.RewardedAdvRequest(showHintsCallback);
        }

        private void ShowAllQuests()
        {
            _panelsManager.OpenPanel(PanelMode.AllQuests);
        }
    }
}
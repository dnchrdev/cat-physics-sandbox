using System;
using System.Collections.Generic;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.UseCases;
using Zenject;

namespace Features.Quests.InterfaceAdapters.Controllers
{
    public sealed class QuestsUIController : IInitializable, IDisposable
    {
        private readonly List<ICurrentQuestView> _currentQuestViews;
        private readonly IAllQuestsView _allQuestsView;
        private readonly IQuestCardsView _cards;
        private readonly RequestQuestHintsUseCase _requestHints;
        private readonly QuestListUseCase _questList;
        private readonly QuestsNavigationUseCase _navigation;
        private readonly SelectQuestUseCase _selectQuest;

        public QuestsUIController(
            List<ICurrentQuestView> currentQuestViews,
            IAllQuestsView allQuestsView,
            IQuestCardsView cards,
            RequestQuestHintsUseCase requestHints,
            QuestListUseCase questList,
            QuestsNavigationUseCase navigation,
            SelectQuestUseCase selectQuest)
        {
            _currentQuestViews = currentQuestViews;
            _allQuestsView = allQuestsView;
            _cards = cards;
            _requestHints = requestHints;
            _questList = questList;
            _navigation = navigation;
            _selectQuest = selectQuest;
        }

        public void Initialize()
        {
            foreach (var view in _currentQuestViews)
            {
                view.HintsRequested += _requestHints.Execute;
                view.AllQuestsRequested += _navigation.OpenQuestList;
            }

            _allQuestsView.PanelEntered += _questList.Show;
            _allQuestsView.PanelExited += _questList.Hide;
            _allQuestsView.CloseRequested += _navigation.CloseQuestList;
            _cards.QuestSelected += _selectQuest.Execute;
        }

        public void Dispose()
        {
            foreach (var view in _currentQuestViews)
            {
                view.HintsRequested -= _requestHints.Execute;
                view.AllQuestsRequested -= _navigation.OpenQuestList;
            }

            _allQuestsView.PanelEntered -= _questList.Show;
            _allQuestsView.PanelExited -= _questList.Hide;
            _allQuestsView.CloseRequested -= _navigation.CloseQuestList;
            _cards.QuestSelected -= _selectQuest.Execute;
        }
    }
}

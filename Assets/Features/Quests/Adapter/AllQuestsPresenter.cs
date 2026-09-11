using System;
using Features.Quests.Adapter.Interfaces;
using Features.Quests.Application;
using Features.Quests.Domain;
using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Data;
using Zenject;

namespace Features.Quests.Adapter
{
    public class AllQuestsPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly QuestsCollection _questsCollection;
        [Inject] private readonly IAllQuestsView _view;
        [Inject] private readonly UIPanelsManager _panelsManager;
        [Inject] private readonly QuestCaldsManager _questCaldsManager;
        
        public void Initialize()
        {
            _view.CloseRequestedEvent += HandleClose;
            _view.PanelEnteredEvent += HandlePanelEntered;
            _view.PanelExitedEvent += HandlePanelExited;

            _questCaldsManager.NewQuestSelected += HandleNewQuestSelected;
        }

        public void Dispose()
        {
            _view.CloseRequestedEvent -= HandleClose;
            _view.PanelEnteredEvent -= HandlePanelEntered;
            _view.PanelExitedEvent -= HandlePanelExited;

            _questCaldsManager.NewQuestSelected -= HandleNewQuestSelected;
        }

        private void HandlePanelEntered()
        {
            _questCaldsManager.CreateAllQuestCards(_view.GetShowedContentParent());
        }

        private void HandlePanelExited()
        {
            _questCaldsManager.HideAllQuestCards(_view.GetHiddenContentParent());
        }

        private void HandleNewQuestSelected(string name)
        {
            _questsCollection.SwitchCurrentQuest(name);
            HandleClose();
        }

        private void HandleClose()
        {
            _panelsManager.OpenPanel(PanelMode.Gameplay);
        }
    }
}
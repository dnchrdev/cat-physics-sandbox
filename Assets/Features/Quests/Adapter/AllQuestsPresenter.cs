using System;
using Feature.UI;
using Zenject;

namespace Feature.Quests
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
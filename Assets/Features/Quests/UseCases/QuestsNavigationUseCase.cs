using Features.Shared.Entities;
using Features.Shared.UseCases;

namespace Features.Quests.UseCases
{
    public sealed class QuestsNavigationUseCase
    {
        private readonly IUIPanelsManager _panels;

        public QuestsNavigationUseCase(IUIPanelsManager panels)
        {
            _panels = panels;
        }

        public void OpenQuestList() => _panels.OpenPanel(PanelMode.AllQuests);
        public void CloseQuestList() => _panels.OpenPanel(PanelMode.Gameplay);
    }
}

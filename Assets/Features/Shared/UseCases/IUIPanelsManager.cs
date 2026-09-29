using Features.Shared.Entities;
using Features.Shared.Utilities;

namespace Features.Shared.UseCases
{
    public interface IUIPanelsManager
    {
        Result RegisterPanel(IPanel panel);
        void OpenPanel(PanelMode panelMode);
        void ClearAllPanels();
    }
}

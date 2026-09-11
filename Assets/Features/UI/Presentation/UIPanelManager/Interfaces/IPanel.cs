using Features.UI.Infrastructure.Data;

namespace Features.UI.Presentation.UIPanelManager.Interfaces
{
    public interface IPanel
    {
        PanelMode[] PanelModes { get; }
        PanelInput PanelInput { get; }
        void InitPanel();
        void OnEnterPanel();
        void OnExitPanel();
    }
}
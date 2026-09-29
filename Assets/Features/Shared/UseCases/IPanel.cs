using Features.Shared.Entities;

namespace Features.Shared.UseCases
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

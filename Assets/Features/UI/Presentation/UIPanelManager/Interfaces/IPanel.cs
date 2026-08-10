namespace Feature.UI
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
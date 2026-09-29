namespace Features.UI.InterfaceAdapters.Ports
{
    public interface ILoadingScreenView
    {
        void ShowLoadingPanel(bool active);
        void SetLoadingScreenAlpha(float alpha);
        void SetLoadingCircleAlpha(float alpha);
        void SetLoadingCircleZAngle(float angle);
        float GetLoadingCircleZAngle();
    }
}

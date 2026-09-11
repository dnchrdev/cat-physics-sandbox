namespace Features.UI.Presentation.Interfaces
{
    public interface ILoadingScreenView
    {
        void ShowLoadingPanel(bool active);
        void SetLoadingScreenAlpha(float aplha);
        void SetLoadingCircleAlpha(float aplha);
        void SetLoadingCircleZAngle(float angle);
        float GetLoadingCircleZAngle();
    }
}
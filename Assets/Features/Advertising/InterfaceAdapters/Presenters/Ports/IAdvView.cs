namespace Features.Advertising.InterfaceAdapters.Presenters.Ports
{
    public interface IAdvView
    {
        void ShowTimer();
        void HideTimer();
        void UpdateTimer(int seconds);
    }
}

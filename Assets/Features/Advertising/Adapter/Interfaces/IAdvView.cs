namespace Features.Advertising.Adapter.Interfaces
{
    public interface IAdvView
    {
        void SetActive(bool isActive);
        void ShowInterstitialPanel(bool isActive);
        void UpdateTimer(int seconds);
    }
}
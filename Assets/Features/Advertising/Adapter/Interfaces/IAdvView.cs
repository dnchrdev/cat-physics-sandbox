using Feature.UI;

namespace Feature.Advertising
{
    public interface IAdvView
    {
        void SetActive(bool isActive);
        void ShowInterstitialPanel(bool isActive);
        void UpdateTimer(int seconds);
    }
}
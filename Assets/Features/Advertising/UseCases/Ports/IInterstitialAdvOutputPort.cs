namespace Features.Advertising.UseCases.Ports
{
    public interface IInterstitialAdvOutputPort
    {
        void PresentCountdownVisibility(bool isVisible);
        void PresentCountdownSeconds(int seconds);
    }
}

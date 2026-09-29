using Features.Advertising.InterfaceAdapters.Presenters.Ports;
using Features.Advertising.UseCases.Ports;
using Zenject;

namespace Features.Advertising.InterfaceAdapters.Presenters
{
    public sealed class InterstitialAdvPresenter : IInitializable, IInterstitialAdvOutputPort
    {
        private readonly IAdvView _view;

        public InterstitialAdvPresenter(IAdvView view)
        {
            _view = view;
        }

        public void Initialize() => _view.HideTimer();

        public void PresentCountdownVisibility(bool isVisible)
        {
            if (isVisible)
                _view.ShowTimer();
            else
                _view.HideTimer();
        }

        public void PresentCountdownSeconds(int seconds) => _view.UpdateTimer(seconds);
    }
}

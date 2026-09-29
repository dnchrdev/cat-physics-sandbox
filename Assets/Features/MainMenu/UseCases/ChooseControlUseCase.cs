using Features.MainMenu.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.MainMenu.UseCases
{
    public sealed class ChooseControlUseCase
    {
        private readonly IControlSettings _controlSettings;
        private readonly IMainMenuOutputPort _output;

        public ChooseControlUseCase(IControlSettings controlSettings, IMainMenuOutputPort output)
        {
            _controlSettings = controlSettings;
            _output = output;
        }

        public void ChoosePC() => Choose(false);
        public void ChooseMobile() => Choose(true);

        private void Choose(bool isMobile)
        {
            _controlSettings.SetIsMobile(isMobile);
            _output.ShowControlChoice(false);
        }
    }
}

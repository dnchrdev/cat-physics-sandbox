using System;
using Features.MobileButtonsAdjustment.Entities;
using Features.MobileButtonsAdjustment.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Storage.Entities.Ports;

namespace Features.MobileButtonsAdjustment.UseCases
{
    public sealed class OpenButtonsAdjustmentUseCase
    {
        private static readonly AdjustableButtonType[] Buttons =
            (AdjustableButtonType[])Enum.GetValues(typeof(AdjustableButtonType));

        private readonly ButtonLayoutEditSession _session;
        private readonly IReadOnlyMobileControls _settings;
        private readonly IButtonVisibilityReader _visibility;
        private readonly IButtonsAdjustmentOutput _output;

        public OpenButtonsAdjustmentUseCase(
            ButtonLayoutEditSession session,
            IReadOnlyMobileControls settings,
            IButtonVisibilityReader visibility,
            IButtonsAdjustmentOutput output)
        {
            _session = session;
            _settings = settings;
            _visibility = visibility;
            _output = output;
        }

        public void Execute()
        {
            _session.Begin();
            var visibility = _visibility.GetAll();

            foreach (var button in Buttons)
            {
                var isVisible = visibility.TryGetValue(button, out var visible) && visible;
                _output.ShowButtonVisibility(button, isVisible);

                if (_settings.TryGetCustomPosition(button, out var position))
                    _output.ShowCustomPosition(button, position);
                else
                    _output.ShowDefaultPosition(button);
            }
        }
    }
}

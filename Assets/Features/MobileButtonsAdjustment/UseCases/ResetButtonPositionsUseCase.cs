using System;
using Features.MobileButtonsAdjustment.Entities;
using Features.MobileButtonsAdjustment.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;

namespace Features.MobileButtonsAdjustment.UseCases
{
    public sealed class ResetButtonPositionsUseCase
    {
        private static readonly AdjustableButtonType[] Buttons =
            (AdjustableButtonType[])Enum.GetValues(typeof(AdjustableButtonType));

        private readonly ButtonLayoutEditSession _session;
        private readonly IMobileControlsSettings _settings;
        private readonly IButtonsAdjustmentOutput _output;

        public ResetButtonPositionsUseCase(
            ButtonLayoutEditSession session,
            IMobileControlsSettings settings,
            IButtonsAdjustmentOutput output)
        {
            _session = session;
            _settings = settings;
            _output = output;
        }

        public void Execute()
        {
            if (_settings.HasCustomPositions)
                _session.MarkChanged();

            _settings.ResetPositions();

            foreach (var button in Buttons)
                _output.ShowDefaultPosition(button);
        }
    }
}

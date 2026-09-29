using Features.MobileButtonsAdjustment.Entities;
using Features.MobileButtonsAdjustment.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.MobileButtonsAdjustment.UseCases
{
    public sealed class MoveButtonUseCase
    {
        private readonly ButtonLayoutEditSession _session;
        private readonly IMobileControlsSettings _settings;
        private readonly IButtonsAdjustmentOutput _output;

        public MoveButtonUseCase(
            ButtonLayoutEditSession session,
            IMobileControlsSettings settings,
            IButtonsAdjustmentOutput output)
        {
            _session = session;
            _settings = settings;
            _output = output;
        }

        public void Execute(AdjustableButtonType button, Vector2 position)
        {
            _settings.SetCustomPosition(button, position);
            _session.MarkChanged();
            _output.ShowCustomPosition(button, position);
        }
    }
}

using Features.Shared.Entities;
using UnityEngine;

namespace Features.MobileButtonsAdjustment.UseCases.Ports
{
    public interface IButtonsAdjustmentOutput
    {
        void ShowButtonVisibility(AdjustableButtonType button, bool isVisible);
        void ShowCustomPosition(AdjustableButtonType button, Vector2 position);
        void ShowDefaultPosition(AdjustableButtonType button);
    }
}

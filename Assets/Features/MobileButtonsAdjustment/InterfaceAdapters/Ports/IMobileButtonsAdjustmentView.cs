using System;
using Features.Shared.Entities;
using UnityEngine;

namespace Features.MobileButtonsAdjustment.InterfaceAdapters.Ports
{
    public interface IMobileButtonsAdjustmentView
    {
        event Action Opened;
        event Action CloseRequested;
        event Action ResetRequested;
        event Action<AdjustableButtonType, Vector2> ButtonDragged;

        void SetButtonVisible(AdjustableButtonType button, bool isVisible);
        void SetButtonPosition(AdjustableButtonType button, Vector2 position);

        void RestoreDefaultPosition(AdjustableButtonType button);
    }
}

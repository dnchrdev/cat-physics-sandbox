using System;
using Feature.Storage;
using UnityEngine;

namespace Feature.PlayerFeature
{
    public interface IMobileControlView : IGameplayControlView
    {
        event Action MobileButtonsAdjustmentOpenedEvent;
        event Action<Vector2> MoveJoystickUpdatedEvent;
        event Action<Vector2> LookJoystickUpdatedEvent;
        event Action JumpButtonPressedEvent;
        event Action JumpButtonReleasedEvent;
        event Action HitButtonPressedEvent;
        event Action GrabButtonPressedEvent;
        event Action ThrowButtonPressedEvent;
        event Action ReleaseButtonPressedEvent;

        void InitMoveJoystick(IReadOnlyMobileControls settings);
        void ApplyButtonPositions(IReadOnlyMobileControls settings);
    }
}
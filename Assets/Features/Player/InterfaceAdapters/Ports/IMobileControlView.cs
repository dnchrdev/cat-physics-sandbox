using System;
using UnityEngine;

namespace Features.Player.InterfaceAdapters.Ports
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
    }
}

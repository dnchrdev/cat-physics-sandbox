using System;
using UnityEngine;

namespace Features.Input.Infrastructure.Interfaces
{
    public interface IMovementInput
    {
        event Action<Vector2> HorizontalMoveEvent;
        event Action JumpStartEvent;
        event Action JumpReleaseEvent;

        void InvokeHorizontalMove(Vector2 input);
        void InvokeJumpStart();
        void InvokeJumpRelease();
    }
}
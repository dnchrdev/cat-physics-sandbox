using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.Input
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
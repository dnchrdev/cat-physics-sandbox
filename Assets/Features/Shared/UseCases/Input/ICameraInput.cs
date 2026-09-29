using System;
using UnityEngine;

namespace Features.Shared.UseCases.Input
{
    public interface ICameraInput
    {
        event Action<Vector2> LookEvent;
        void InvokeLook(Vector2 lookDelta);
    }
}
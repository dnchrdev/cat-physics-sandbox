using System;
using UnityEngine;

namespace Features.Input.Infrastructure.Interfaces
{
    public interface ICameraInput
    {
        event Action<Vector2> LookEvent;

        void InvokeLook(Vector2 lookDelta);
    }
}
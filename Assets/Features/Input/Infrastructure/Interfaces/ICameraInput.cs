using System;
using System.Collections;
using UnityEngine;

namespace Feature.Input
{
    public interface ICameraInput
    {
        event Action<Vector2> LookEvent;

        void InvokeLook(Vector2 lookDelta);
    }
}
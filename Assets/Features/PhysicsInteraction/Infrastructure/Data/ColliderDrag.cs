using System;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    [Serializable]
    public struct ColliderDrag
    {
        [field: SerializeField] public float Drag { get; private set; }
        [field: SerializeField] public float AngularDrag { get; private set; }
    }

}
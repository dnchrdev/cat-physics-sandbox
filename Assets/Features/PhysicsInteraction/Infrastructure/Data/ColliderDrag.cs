using System;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Data
{
    [Serializable]
    public struct ColliderDrag
    {
        [field: SerializeField] public float Drag { get; private set; }
        [field: SerializeField] public float AngularDrag { get; private set; }
    }

}
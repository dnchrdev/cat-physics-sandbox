using System;
using System.Collections;
using Feature.CameraFeature;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IGrabable
    {
        bool Grab(Rigidbody hand, Rigidbody rb, Collider collider, Vector3 grabPosition);
        bool IsGrabbed();
        void Throw(Vector3 toPoint, float power, Rigidbody rb);
        void Release();
        Vector3 GetGrabPosition(Vector3 handPosition, IReadOnlyCamera camera, LayerMask interactionMask);
        void UpdateAnchor(Vector3 grabPosition);
    }
}
using System;
using Feature.CameraFeature;
using Feature.Core;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class GrabNearestPointStrategy : GrabStrategyBase
    {
        private const float MAX_RAYCAST_DISTANCE = 100f;
        
        public GrabNearestPointStrategy(DestroyService ds) : base(ds) { }

        protected override void ConfigureJoint(
            ConfigurableJoint joint, JointDrive drive, Vector3 impactPosition)
        {
            joint.anchor = Transform.InverseTransformPoint(impactPosition);
        }
        
        public override Vector3 GetGrabPosition(Vector3 handPosition, IReadOnlyCamera camera, LayerMask interactionMask)
        {
            var closestToHandPosition = GetClosestGrabPosition(handPosition);
            var grabPosition = GetClosestReycastedPoint(camera, interactionMask, Collider, closestToHandPosition);
            Joint.anchor = Transform.InverseTransformPoint(grabPosition);
            return grabPosition;
        }

        public override void UpdateAnchor(Vector3 grabPosition)
        {
            Joint.anchor = Transform.InverseTransformPoint(grabPosition);
        }
        
        private Vector3 GetClosestGrabPosition(Vector3 handPosition)
        {
            return Physics.ClosestPoint(
                handPosition,
                Collider,
                Transform.position,
                Transform.rotation);
        }
        
        public Vector3 GetClosestReycastedPoint(IReadOnlyCamera camera, LayerMask interactionMask, Collider grabbedCollider, Vector3 fallback)
        {
            var hits = Physics.RaycastAll(camera.Position, camera.Forward, MAX_RAYCAST_DISTANCE, interactionMask);

            if (hits.Length == 0) return fallback;

            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (var hit in hits)
                if (hit.collider == grabbedCollider)
                    return hit.point;

            return fallback;
        }
        
    }
}
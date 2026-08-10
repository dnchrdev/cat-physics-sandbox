using System;
using Feature.CameraFeature;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Feature.PhysicsInteraction
{
    public class InteractionDetector
    {
        private const float MAX_RAYCAST_DISTANCE = 100f;
        
        private readonly IReadOnlyCamera _camera;
        private readonly InteractionControllerConfig _config;

        public InteractionDetector(IReadOnlyCamera camera, InteractionControllerConfig config)
        {
            _camera = camera;
            _config = config;
        }

        public FocusDetectionResult FocusDetect()
        {
            var start = _camera.Position;
            var end = start + _camera.Forward * _config.FocusDistance;

            if (Physics.Linecast(start, end, out var hit,
                    _config.InteractionMask, QueryTriggerInteraction.Collide))
                return new FocusDetectionResult(hit.collider, hit.point);

            return TrySphereFallback(start, end);
        }

        private FocusDetectionResult TrySphereFallback(Vector3 start, Vector3 end)
        {
            var direction = (end - start).normalized;

            if (!Physics.SphereCast(start, _config.FocusSphereCastRadius, direction,
                    out var hit, _config.FocusDistance,
                    _config.InteractionMask, QueryTriggerInteraction.Collide))
                return FocusDetectionResult.None;

            var closestPoint = hit.collider.ClosestPoint(
                _camera.Position + direction * hit.distance);

            return new FocusDetectionResult(hit.collider, closestPoint);
        }

        public Vector3 GetHitAimingPoint()
        {
            var start = _camera.Position;
            var direction = _camera.Forward;
            var fallback = start + direction * MAX_RAYCAST_DISTANCE;

            if (Physics.Raycast(start, direction, out var hitInfo, MAX_RAYCAST_DISTANCE, _config.AimingPointMask))
                return hitInfo.point;
            return fallback;
        }

        public Vector3 GetDirection(Vector3 aimigPoint)
        {
            return (aimigPoint - _camera.Position).normalized;   
        }
        
        public Vector3 GetThrowAimingPoint(Collider excludedCollider)
        {
            var start = _camera.Position;
            var direction = _camera.Forward;
            var fallback = start + direction * MAX_RAYCAST_DISTANCE;

            var hits = Physics.RaycastAll(start, direction, MAX_RAYCAST_DISTANCE, _config.AimingPointMask);
            if (hits.Length == 0) return fallback;

            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (var hit in hits)
                if (hit.collider != excludedCollider)
                    return hit.point;

            return fallback;
        }
        
        
    }
}
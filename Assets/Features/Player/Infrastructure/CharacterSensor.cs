using System.Collections.Generic;
using UnityEngine;

namespace Features.Player.Infrastructure
{
    public class CharacterSensor
    {
        private readonly Transform _transform;
        private readonly List<Collider> _ignoreList;
        private readonly int _ignoreRaycastLayer;
        private readonly bool _calculateRealSurfaceNormal;
        private readonly bool _calculateRealHitDistance;
        private readonly int _groundLayerMask;

        private readonly List<Collider> _hitColliders = new();

        private float _sphereCastRadius = 0.2f;
        private Vector3 _origin = Vector3.zero;
        private bool _hasDetectedHit;
        private Vector3 _hitPosition;
        private Vector3 _hitNormal;
        private float _hitDistance;
        private Vector3 _backupNormal;
        private int[] _ignoreListLayers;

        public CharacterSensor(
            Transform transform,
            List<Collider> ignoreColliders,
            bool calculateRealSurfaceNormal,
            bool calculateRealHitDistance,
            int groundLayerMask)
        {
            _transform = transform;
            _ignoreList = ignoreColliders;
            _ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
            _ignoreListLayers = new int[_ignoreList.Count];
            _calculateRealSurfaceNormal = calculateRealSurfaceNormal;
            _calculateRealHitDistance = calculateRealHitDistance;
            _groundLayerMask = groundLayerMask;
        }

        public void SetSphereRadius(float newRadius)
        {
            _sphereCastRadius = newRadius;
        }

        public bool HasDetectedHit() => _hasDetectedHit;
        public float GetDistance() => _hitDistance;
        public Vector3 GetNormal() => _hitNormal;

        public Collider GetCollider()
        {
            return _hitColliders.Count == 0 ? null : _hitColliders[0];
        }

        public void SetCastOrigin(Vector3 origin)
        {
            if (_transform == null)
                return;

            _origin = _transform.InverseTransformPoint(origin);
        }

        public void Cast(float realDetectionDistance)
        {
            ResetFlags();

            Vector3 worldDirection = GetCastDirection();
            Vector3 worldOrigin = _transform.position;

            if (_ignoreListLayers.Length != _ignoreList.Count)
                _ignoreListLayers = new int[_ignoreList.Count];

            for (int i = 0; i < _ignoreList.Count; i++)
            {
                _ignoreListLayers[i] = _ignoreList[i].gameObject.layer;
                _ignoreList[i].gameObject.layer = _ignoreRaycastLayer;
            }

            _hasDetectedHit = CastSphere(worldOrigin, worldDirection, realDetectionDistance, _groundLayerMask);

            for (int i = 0; i < _ignoreList.Count; i++)
                _ignoreList[i].gameObject.layer = _ignoreListLayers[i];
        }

        private void ResetFlags()
        {
            _hasDetectedHit = false;
            _hitPosition = Vector3.zero;
            _hitNormal = -GetCastDirection();
            _hitDistance = 0f;

            if (_hitColliders.Count > 0)
                _hitColliders.Clear();
        }

        private bool CastSphere(Vector3 origin, Vector3 direction, float realDetectionDistance, int layerMask)
        {
            var hasHit = UnityEngine.Physics.SphereCast(
                origin,
                _sphereCastRadius,
                direction,
                out RaycastHit hit,
                realDetectionDistance,
                layerMask,
                QueryTriggerInteraction.Ignore);

            bool hitIsWithinRange = hasHit && hit.distance + _sphereCastRadius <= realDetectionDistance;
            if (!hitIsWithinRange)
                return false;

            _hitPosition = hit.point;
            _hitNormal = hit.normal;
            _hasDetectedHit = true;
            _hitDistance = hit.distance + _sphereCastRadius;
            _hitColliders.Add(hit.collider);

            if (_calculateRealHitDistance)
                _hitDistance = ExtractDotVector(origin - _hitPosition, direction).magnitude;

            if (_calculateRealSurfaceNormal)
                _hitNormal = CalculateRealSurfaceNormal(_hitPosition, direction, _hitNormal);

            return true;
        }

        private Vector3 CalculateRealSurfaceNormal(Vector3 hitPosition, Vector3 direction, Vector3 fallbackNormal)
        {
            Collider collider = _hitColliders[0];
            var ray = new Ray(hitPosition - direction, direction);

            if (collider.Raycast(ray, out RaycastHit hit, 1.5f))
            {
                if (Vector3.Angle(hit.normal, -direction) >= 89f)
                    return _backupNormal;

                _backupNormal = hit.normal;
                return hit.normal;
            }

            return _backupNormal;
        }

        private static Vector3 ExtractDotVector(Vector3 vector, Vector3 direction)
        {
            if (direction.sqrMagnitude != 1)
                direction.Normalize();

            float amount = Vector3.Dot(vector, direction);
            return direction * amount;
        }

        private static Vector3 GetCastDirection()
        {
            return Vector3.down;
        }
    }
}

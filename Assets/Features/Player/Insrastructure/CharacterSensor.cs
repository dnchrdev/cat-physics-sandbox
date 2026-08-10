using System.Collections.Generic;
using UnityEngine;

namespace Feature.PlayerFeature
{
    public class CharacterSensor
    {
        private float _sphereCastRadius = 0.2f;
        private Vector3 _origin = Vector3.zero;

        private bool _hasDetectedHit;
        private Vector3 _hitPosition;
        private Vector3 _hitNormal;
        private float _hitDistance;

        private List<Collider> _hitColliders = new List<Collider>();
        private List<Transform> _hitTransforms = new List<Transform>();

        private Vector3 _backupNormal;

        private Transform _tr;

        private int _ignoreRaycastLayer;

        private bool _calculateRealSurfaceNormal = false;
        private bool _calculateRealHitDistance = false;

        private List<Collider> _ignoreList;

        private int[] _ignoreListLayers;
        private int _groundLayerMask;

        public CharacterSensor(
            Transform transform,
            List<Collider> ignoreColliders,
            bool collideRealSurfaceNormal,
            bool calculateRealHitDistance,
            int groundLayermask)
        {
            _tr = transform;

            _ignoreList = ignoreColliders;

            _ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");

            _ignoreListLayers = new int[_ignoreList.Count];

            _calculateRealSurfaceNormal = collideRealSurfaceNormal;
            _calculateRealHitDistance = calculateRealHitDistance;
            _groundLayerMask = groundLayermask;
            
        }

        public void SetSphereRadius(float newRadius)
        {
            _sphereCastRadius = newRadius;
        }

        public bool HasDetectedHit() => _hasDetectedHit;
        public float GetDistance() => _hitDistance;

        public Vector3 GetNormal() => _hitNormal;

        public Vector3 GetPosition() => _hitPosition;

        public Collider GetCollider()
        {
            if (_hitColliders.Count == 0) return null;
            return _hitColliders[0];
        }

        public Transform GetTransform() => _hitTransforms[0];

        public void SetCastOrigin(Vector3 origin)
        {
            if (_tr == null)
                return;

            _origin = _tr.InverseTransformPoint(origin);
        }

        public void Cast(float realDetectionDistance)
        {
            ResetFlags();

            Vector3 worldDirection = GetCastDirection();
            Vector3 worldOrigin = _tr.position;

            if (_ignoreListLayers.Length != _ignoreList.Count)
            {
                _ignoreListLayers = new int[_ignoreList.Count];
            }

            for (int i = 0; i < _ignoreList.Count; i++)
            {
                _ignoreListLayers[i] = _ignoreList[i].gameObject.layer;
                _ignoreList[i].gameObject.layer = _ignoreRaycastLayer;
            }

            _hasDetectedHit = CastSphere(worldOrigin, worldDirection, realDetectionDistance, _groundLayerMask);


            for (int i = 0; i < _ignoreList.Count; i++)
            {
                _ignoreList[i].gameObject.layer = _ignoreListLayers[i];
            }
        }

        private void ResetFlags()
        {
            _hasDetectedHit = false;
            _hitPosition = Vector3.zero;
            _hitNormal = -GetCastDirection();
            _hitDistance = 0f;

            if (_hitColliders.Count > 0)
                _hitColliders.Clear();
            if (_hitTransforms.Count > 0)
                _hitTransforms.Clear();
        }

        private bool CastSphere(Vector3 origin, Vector3 direction, float realDetectionDistance, int layerMask)
        {
            var hasHit = Physics.SphereCast(
                origin,
                _sphereCastRadius,
                direction,
                out RaycastHit _hit,
                realDetectionDistance,
                layerMask,
                QueryTriggerInteraction.Ignore);

            bool hitIsWithinRange = hasHit && _hit.distance + _sphereCastRadius <= realDetectionDistance;
            
            if (hitIsWithinRange)
            {
                _hitPosition = _hit.point;
                _hitNormal = _hit.normal;
                _hasDetectedHit = true;
                _hitDistance = _hit.distance + _sphereCastRadius;

                _hitColliders.Add(_hit.collider);
                _hitTransforms.Add(_hit.transform);

                if (_calculateRealHitDistance)
                    _hitDistance = ExtractDotVector(origin - _hitPosition, direction).magnitude;


                if (_calculateRealSurfaceNormal)
                    _hitNormal = CalculateRealSurfaceNormal(_hitPosition, direction, _hitNormal);

                return true;
            }
            else
            {
                return false;
            }
        }

        private Vector3 CalculateRealSurfaceNormal(Vector3 hitPosition, Vector3 direction, Vector3 fallbackNormal)
        {
            Collider col = _hitColliders[0];
            var ray = new Ray(hitPosition - direction, direction);

            if (col.Raycast(ray, out RaycastHit hit2, 1.5f))
            {
                if (Vector3.Angle(hit2.normal, -direction) >= 89f)
                {
                    _backupNormal = _backupNormal;
                    return _backupNormal;
                }

                _backupNormal = hit2.normal;
                return hit2.normal;
            }

            return _backupNormal;
        }

        private Vector3 ExtractDotVector(Vector3 _vector, Vector3 _direction)
        {
            if (_direction.sqrMagnitude != 1)
                _direction.Normalize();

            float _amount = Vector3.Dot(_vector, _direction);

            return _direction * _amount;
        }

        private Vector3 GetCastDirection()
        {
            return Vector3.down;
        }
    }
}
using System.Collections.Generic;
using Features.Player.Entities;
using Features.Player.UseCases.Ports;
using UnityEngine;
using Zenject;

namespace Features.Player.Infrastructure
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class CharacterMotor : MonoBehaviour, ICharacterMotor, IInitializable
    {
        private const float FloatAdjustmentStrength = 0.1f;

        [Header("Floating")]
        [Range(0f, 2f)] [SerializeField] private float _floatingDistance = 0.25f;
        [Range(0f, 1f)] [SerializeField] private float _stepDownDistance = 0.25f;

        [Header("Collider:")]
        [Range(0f, 1f)] [SerializeField] private float _colliderHeight = 2f;
        [Range(0f, 1f)] [SerializeField] private float _colliderRadius = 1f;
        [SerializeField] private Vector3 _colliderOffset = Vector3.zero;

        [Header("Sensor:")]
        [SerializeField] private float _stableAngle = 45f;
        [SerializeField] private LayerMask _groundMask;
        [Range(0f, 1f)] [SerializeField] private float _sensorRadius = 0.075f;
        [SerializeField] private List<Collider> _ignoreColliders;
        [SerializeField] private bool _calculateRealSurfaceNormal = true;
        [SerializeField] private bool _calculateRealHitDistance = true;

        private CapsuleCollider _capsuleCollider;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private CharacterSensor _sensor;
        
        public GameObject GroundObject
        {
            get
            {
                var groundCollider = _sensor?.GetCollider();
                return groundCollider == null ? null : groundCollider.gameObject;
            }
        }

        public void Initialize()
        {
            Setup();

            _ignoreColliders.Add(_collider);
            _sensor = new CharacterSensor(
                transform,
                _ignoreColliders,
                _calculateRealSurfaceNormal,
                _calculateRealHitDistance,
                _groundMask);

            RecalculateColliderDimensions();
            RecalibrateSensor();
        }

        private void OnValidate()
        {
            if (gameObject.activeInHierarchy)
                RecalculateColliderDimensions();
        }

        public Vector3 GetPosition() => transform.position;
        public Quaternion GetRotation() => transform.rotation;

        public Vector3 GetVelocity() => _rigidbody.linearVelocity;
        public void SetVelocity(Vector3 velocity) => _rigidbody.linearVelocity = velocity;

        public void Teleport(Vector3 position, Quaternion rotation)
        {
            _rigidbody.position = position;
            _rigidbody.rotation = rotation;
        }

        public GroundProbe ProbeGround(bool withStepDown)
        {
            var castLength = withStepDown ? _floatingDistance + _stepDownDistance : _floatingDistance;
            _sensor.Cast(castLength);

            if (!_sensor.HasDetectedHit())
                return GroundProbe.None;

            var normal = _sensor.GetNormal();
            var isStable = Vector3.Angle(normal, Vector3.up) <= _stableAngle;

            return new GroundProbe(isGrounded: true, isStable, normal, FloatAdjustmentVelocity(normal));
        }
        
        private Vector3 FloatAdjustmentVelocity(Vector3 normal)
        {
            var distanceToGo = _floatingDistance - _sensor.GetDistance();

            var currentVelocity = _rigidbody.linearVelocity;
            var horizontalVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

            var sinkRate = -Vector3.Dot(horizontalVelocity, normal) / normal.y;
            var baseAdjustment = distanceToGo / Time.fixedDeltaTime * FloatAdjustmentStrength;

            return Vector3.up * (baseAdjustment + sinkRate);
        }

        private void Setup()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _rigidbody.useGravity = false;
        }

        private void RecalculateColliderDimensions()
        {
            if (_collider == null)
            {
                Setup();

                if (_collider == null)
                {
                    Debug.LogWarning("There is no collider attached to " + gameObject.name + "!");
                    return;
                }
            }

            if (_capsuleCollider)
            {
                _capsuleCollider.height = _colliderHeight;
                _capsuleCollider.center = new Vector3(
                    Mathf.Max(_colliderOffset.x, 0f),
                    Mathf.Max(_colliderOffset.y, 0f),
                    Mathf.Max(_colliderOffset.z, 0f)
                );
                _capsuleCollider.radius = _colliderRadius;

                if (_capsuleCollider.height / 2f < _capsuleCollider.radius)
                    _capsuleCollider.radius = _capsuleCollider.height / 2f;
            }

            if (_sensor != null)
                RecalibrateSensor();
        }

        private void RecalibrateSensor()
        {
            _sensor.SetCastOrigin(GetColliderCenter());
            _sensor.SetSphereRadius(_sensorRadius);
        }

        private Vector3 GetColliderCenter()
        {
            if (_collider == null)
                Setup();

            return _collider.bounds.center;
        }
    }
}

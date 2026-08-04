using System.Collections.Generic;
using UnityEngine;

namespace Feature.PlayerFeature
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class CharacterMotor : MonoBehaviour, ICharacterMotor, ICharacterMotorReset, IReadOnlyCharacterMotor
    {
        [Header("Floating")] [Range(0f, 2f)] [SerializeField]
        private float _floatingDistance = 0.25f;

        [Range(0f, 1f)] [SerializeField] private float _stepDownDistance = 0.25f;

        [Header("Collider:")] [Range(0f, 1f)] [SerializeField]
        private float _colliderHeight = 2f;

        [Range(0f, 1f)] [SerializeField] private float _colliderRadius = 1f;
        [SerializeField] private Vector3 _colliderOffset = Vector3.zero;

        private CapsuleCollider _capsuleCollider;

        [Header("Sensor:")] [SerializeField] private float _stableAngle = 45f;
        [SerializeField] private LayerMask _groundMask;
        [Range(0f, 1f)] [SerializeField] private float _sensorRadius = 0.075f;

        private bool _isGrounded = false;
        private bool _isStable = false;
        private bool _isUsingExtendedSensorRange = true;
        private Vector3 _currentGroundAdjustmentVelocity = Vector3.zero;

        [SerializeField] private List<Collider> _ignoreColliders;
        [SerializeField] private bool _calculateRealSurfaceNormal = true;
        [SerializeField] private bool _calculateRealHitDistance = true;

        private Collider _collider;
        private Rigidbody _rb;
        private Transform _tr;
        private CharacterSensor _sensor;

        private void Awake()
        {
            Setup();

            _ignoreColliders.Add(_collider);
            _sensor = new CharacterSensor(this._tr, _ignoreColliders, _calculateRealSurfaceNormal,
                _calculateRealHitDistance, _groundMask);

            RecalculateColliderDimensions();
            RecalibrateSensor();
        }

        private void OnValidate()
        {
            if (this.gameObject.activeInHierarchy)
                RecalculateColliderDimensions();
        }

        public Vector3 GetPosition() => transform.position;
        public Quaternion GetRotation() => transform.rotation;

        public void SetPosition(Vector3 newPos)
        {
            _rb.position = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            _rb.rotation = newRot;
        }

        public void SetFloatingDistance(float distance)
        {
            _floatingDistance = distance;
        }

        public void SetStepDownDistance(float distance)
        {
            _stepDownDistance = distance;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rb.linearVelocity = velocity;
        }

        public Vector3 GetVelocity() => _rb.linearVelocity;

        public void CheckForGround()
        {
            Check();
        }

        public bool IsGrounded() => _isGrounded;

        public bool IsStable() => _isStable;

        public void SetForcedGrounded(bool groounded)
        {
            _isGrounded = groounded;
        }

        public void SetExtendSensorRange(bool _isExtended)
        {
            _isUsingExtendedSensorRange = _isExtended;
        }

        public float GetFloatingDistance() => _floatingDistance;
        public float GetStepDownDistance() => _stepDownDistance;
        public Vector3 GetGroundNormal() => IsGrounded() ? _sensor.GetNormal() : Vector3.up;

        public Vector3 GetGroundPoint() => _sensor.GetPosition();

        public Collider GetGroundCollider() => _sensor.GetCollider();

        public Vector3 GetGroundAdjustmentVelocity() => _currentGroundAdjustmentVelocity;

        private void Setup()
        {
            _tr = transform;
            _collider = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _rb.useGravity = false;
        }

        private void RecalculateColliderDimensions()
        {
            if (_collider == null)
            {
                Setup();

                if (_collider == null)
                {
                    Debug.LogWarning("There is no collider attached to " + this.gameObject.name + "!");
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

        private void Check()
        {
            _currentGroundAdjustmentVelocity = Vector3.zero;

            var castLength = 0f;
            if (_isUsingExtendedSensorRange)
                castLength = _floatingDistance + _stepDownDistance;
            else
                castLength = _floatingDistance;

            _sensor.Cast(castLength);

            if (!_sensor.HasDetectedHit())
            {
                _isStable = false;
                _isGrounded = false;
                return;
            }

            float angle = Vector3.Angle(_sensor.GetNormal(), Vector3.up);

            _isStable = angle <= _stableAngle;
            _isGrounded = true;

            float distance = _sensor.GetDistance();
            float distanceToGo = _floatingDistance - distance;

            _currentGroundAdjustmentVelocity = Vector3.zero;


            if (IsGrounded())
            {
                Vector3 normal = _sensor.GetNormal();
                Vector3 currentVelocity = _rb.linearVelocity;

                Vector3 horizontalVel = new Vector3(currentVelocity.x, 0f, currentVelocity.z);
                float sinkRate = -Vector3.Dot(horizontalVel, normal) / normal.y;


                float baseAdj = distanceToGo / Time.fixedDeltaTime * 0.1f;
                float adjMagnitude = baseAdj + sinkRate;
                _currentGroundAdjustmentVelocity = Vector3.up * adjMagnitude;
            }
        }
    }
}
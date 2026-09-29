using System;
using Features.Interaction.Entities;
using Features.Interaction.Infrastructure.Collision;
using Features.Interaction.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace Features.Interaction.Infrastructure.Interactable
{
    public sealed class InteractableBehaviour : MonoBehaviour
    {
        public event Action Hit;
        public event Action Thrown;
        public event Action<UnityEngine.Collision> Collided;

        [SerializeField] private InteractableConfig _config;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;
        [SerializeField] private HitEnter _colliderHitEvent;
        [SerializeField] private TriggerEnter _colliderTriggerEvent;

        private InteractableFactory _factory;
        private EntityBindResolver _bindResolver;

        public InteractableEntity Entity { get; private set; }
        public Rigidbody GetRigidbody() => _rb;

        [Inject]
        private void Construct(InteractableFactory factory, EntityBindResolver bindResolver)
        {
            _factory = factory;
            _bindResolver = bindResolver;
        }

        private void Awake()
        {
            if (_config == null || _config.IsRagdoll) return;
            Initialize(_config, _rb, _meshRenderer, _collider, _colliderHitEvent, _colliderTriggerEvent);
        }

        public void Initialize(
            InteractableConfig config,
            Rigidbody rb,
            MeshRenderer meshRenderer,
            Collider collider,
            HitEnter hitEnter,
            TriggerEnter triggerEnter)
        {
            _config = config;
            _rb = rb;
            _meshRenderer = meshRenderer;
            _collider = collider;
            _colliderHitEvent = hitEnter;
            _colliderTriggerEvent = triggerEnter;

            Entity = _factory.Create(_config, _rb, _meshRenderer, _collider);
            Entity.Threw += OnThrew;
            Entity.Hit += OnHit;

            if (_colliderHitEvent != null) _colliderHitEvent.OnHitEvent += OnUnityCollision;
            if (_colliderTriggerEvent != null) _colliderTriggerEvent.OnTriggerEnterEvent += OnUnityTrigger;
        }

        public void Release() => Entity.Release();

        private void OnDestroy()
        {
            if (Entity == null) return;

            Entity.Threw -= OnThrew;
            Entity.Hit -= OnHit;
            if (_colliderHitEvent != null) _colliderHitEvent.OnHitEvent -= OnUnityCollision;
            if (_colliderTriggerEvent != null) _colliderTriggerEvent.OnTriggerEnterEvent -= OnUnityTrigger;
        }

        private void OnThrew() => Thrown?.Invoke();
        private void OnHit() => Hit?.Invoke();

        private void OnUnityCollision(UnityEngine.Collision collision)
        {
            var wasGrabbedAtImpact = Entity.IsGrabbed;
            var impactSpeed = _rb != null ? _rb.linearVelocity.magnitude : 0f;

            Entity.HandleCollision(_bindResolver.ResolveAsTarget(collision.collider), impactSpeed);

            if (!wasGrabbedAtImpact)
                Collided?.Invoke(collision);
        }

        private void OnUnityTrigger(Collider other)
        {
            Entity.HandleTrigger(_bindResolver.ResolveAsTarget(other));
        }
    }
}

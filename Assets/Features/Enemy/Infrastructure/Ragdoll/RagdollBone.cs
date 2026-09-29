using System;
using Features.Core.Infrastructure;
using Features.Enemy.Infrastructure.Configs;
using Features.Interaction.Infrastructure.Collision;
using Features.Interaction.Infrastructure.Interactable;
using UnityEngine;
using Zenject;

namespace Features.Enemy.Infrastructure.Ragdoll
{
    public class RagdollBone : MonoBehaviour
    {
        [SerializeField] private EntityWorldBind _entityWorldBind;

        private Rigidbody _rb;
        private Collider _col;
        private HitEnter _hitEnter;
        private DiContainer _container;
        private InteractableBehaviour _interactable;

        public EntityWorldBind EntityWorldBind => _entityWorldBind;
        public bool IsGrabbed => _interactable != null && _interactable.Entity.IsGrabbed;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<Collider>();
            _hitEnter = GetComponent<HitEnter>();
        }

        public void RagdollEnable(RagdollConfig config)
        {
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _col.material = config.RagdollMaterial;

            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            if (_interactable != null)
                Destroy(_interactable);

            _interactable = gameObject.AddComponent<InteractableBehaviour>();
            _container.Inject(_interactable);
            _interactable.Initialize(config.InteractableConfig, _rb, null, _col, _hitEnter, null);
        }

        public void RagdollDisable(RagdollConfig config)
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
            _col.material = config.DefaultMaterial;

            _rb.interpolation = RigidbodyInterpolation.None;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

            if (_interactable != null)
            {
                _interactable.Release();
                Destroy(_interactable);
                _interactable = null;
            }
        }

        public void SubscribeDisturbed(Action onDisturbed)
        {
            if (_interactable == null) return;
            _interactable.Entity.Released += onDisturbed;
            _interactable.Entity.Hit += onDisturbed;
            _interactable.Entity.Threw += onDisturbed;
            _interactable.Entity.ColliderHit += onDisturbed;
        }

        public void UnsubscribeDisturbed(Action onDisturbed)
        {
            if (_interactable == null) return;
            _interactable.Entity.Released -= onDisturbed;
            _interactable.Entity.Hit -= onDisturbed;
            _interactable.Entity.Threw -= onDisturbed;
            _interactable.Entity.ColliderHit -= onDisturbed;
        }
    }
}

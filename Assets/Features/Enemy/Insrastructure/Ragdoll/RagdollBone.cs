using Feature.Core;
using Feature.PhysicsInteraction;
using Feature.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Feature.EnemyFeature
{
    public class RagdollBone : MonoBehaviour
    {
        public Interactable Events { get; private set; }

        [SerializeField] private EntityWorldBind _entityWorldBind;
        private Rigidbody _rb;
        private Collider _col;
        private HitEnter _hitEnter;
        public EntityWorldBind EntityWorldBind => _entityWorldBind;

        private DiContainer _container;

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

        public void RagdollEnable(PhysicsMaterial physicMaterial, InteractableConfig interactableConfig)
        {
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _col.material = physicMaterial;

            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            if (Events != null)
                Destroy(Events);

            Events = gameObject.AddComponent<Interactable>();
            _container.Inject(Events);
            Events.Initialize(interactableConfig, _rb, null, _col, _hitEnter);
        }
        public void RagdollDisable(PhysicsMaterial physicMaterial)
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
            _col.material = physicMaterial;

            _rb.interpolation = RigidbodyInterpolation.None;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

            if (Events != null) 
            {
                Events.Release();
                Destroy(Events);
            }
        }
    }
}
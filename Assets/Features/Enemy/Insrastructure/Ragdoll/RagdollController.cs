using System;
using Feature.PhysicsInteraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.EnemyFeature
{
    public class RagdollController : MonoBehaviour
    {
        public float TimerSinceLastInteraction { get; private set; }

        [SerializeField] private InteractableConfig _ragdollInteractableConfig;
        [SerializeField] private InteractablePhysicsConfig _interactablePhysicsMaterialConfig;
        [SerializeField] private Transform _enemyRig;
        
        private List<RagdollBone> _ragdollBones = new();

        private bool _active = false;

        private void OnValidate()
        {
            if (_ragdollInteractableConfig == null || _interactablePhysicsMaterialConfig == null) throw new NullReferenceException("Configs cannot be null.");
            if(_enemyRig == null) throw new NullReferenceException("EnemyRig is not set.");
        }

        private void Start()
        {
            var ragdollBones = _enemyRig.GetComponentsInChildren<RagdollBone>();
            
            _ragdollBones.Clear();
            _ragdollBones.AddRange(ragdollBones);
            
            if(_ragdollBones.Count == 0) throw new Exception("Bone count cannot be zero.");
        }

        private void Update()
        {
            if (_active)
            {
                TimerSinceLastInteraction += Time.deltaTime;
            }
        }

        public void EnableRagdoll()
        {
            if(_active) return;

            _active = true;
            TimerSinceLastInteraction = 0f;

            foreach (var bone in _ragdollBones)
            {
                bone.RagdollEnable(_interactablePhysicsMaterialConfig.InteractMaterial, _ragdollInteractableConfig);

                var interactable = bone.Events;
                if (interactable != null)
                {
                    interactable.Released += ResetTimer;
                    interactable.Hitted += ResetTimer;
                    interactable.Throwed += ResetTimer;
                    interactable.ColliderHit += ResetTimer;
                }        
            }

        }

        public void DisableRagdoll()
        {
            if (_active == false) return;

            _active = false;

            foreach (var bone in _ragdollBones)
            {
                var interactable = bone.Events;
                if (interactable != null)
                {
                    interactable.Released -= ResetTimer;
                    interactable.Hitted -= ResetTimer;
                    interactable.Throwed -= ResetTimer;
                    interactable.ColliderHit -= ResetTimer;
                }

                bone.RagdollDisable(_interactablePhysicsMaterialConfig.DefaultMaterial);
            }
        }

        public bool IsAnyBoneGrabbed()
        {
            foreach (var bone in _ragdollBones)
            {
                if(bone.Events.IsGrabbed) return true;
            }
            return false;
        }

        private void ResetTimer()
        {
            TimerSinceLastInteraction = 0f;
        }
    }
}
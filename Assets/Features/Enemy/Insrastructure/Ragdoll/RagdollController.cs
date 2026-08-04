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
        [SerializeField] private PhysicsConfig _physicsMaterialConfig;
        [SerializeField] private List<RagdollBone> _rigdollBones = new();

        private bool _active = false;


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

            foreach (var bone in _rigdollBones)
            {
                bone.RagdollEnable(_physicsMaterialConfig.InteractMaterial, _ragdollInteractableConfig);

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

            foreach (var bone in _rigdollBones)
            {
                var interactable = bone.Events;
                if (interactable != null)
                {
                    interactable.Released -= ResetTimer;
                    interactable.Hitted -= ResetTimer;
                    interactable.Throwed -= ResetTimer;
                    interactable.ColliderHit -= ResetTimer;
                }

                bone.RagdollDisable(_physicsMaterialConfig.DefaultMaterial);
            }
        }

        public bool IsAnyBoneGrabbed()
        {
            foreach (var bone in _rigdollBones)
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
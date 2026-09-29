using System;
using System.Collections.Generic;
using Features.Enemy.Infrastructure.Configs;
using Features.Enemy.InterfaceAdapters.Ports;
using Features.Enemy.UseCases.Ports;
using UnityEngine;

namespace Features.Enemy.Infrastructure.Ragdoll
{
    public class RagdollController : MonoBehaviour, IRagdoll, IRagdollEvents
    {
        public event Action Disturbed;

        [SerializeField] private RagdollConfig _ragdollConfig;
        [SerializeField] private Transform _enemyRig;

        private readonly List<RagdollBone> _bones = new();
        private bool _isEnabled;

        public bool IsAnyBoneGrabbed
        {
            get
            {
                foreach (var bone in _bones)
                    if (bone.IsGrabbed)
                        return true;
                return false;
            }
        }

        private void OnValidate()
        {
            if (_ragdollConfig == null)
                Debug.LogWarning($"{name}: RagdollConfig is not set.", this);
            if (_enemyRig == null)
                Debug.LogWarning($"{name}: Enemy rig is not set.", this);
        }

        private void Start()
        {
            _bones.Clear();
            _bones.AddRange(_enemyRig.GetComponentsInChildren<RagdollBone>());

            if (_bones.Count == 0)
                throw new Exception($"{name}: no RagdollBone found under the enemy rig.");
        }

        public void Enable()
        {
            if (_isEnabled) return;
            _isEnabled = true;

            foreach (var bone in _bones)
            {
                bone.RagdollEnable(_ragdollConfig);
                bone.SubscribeDisturbed(RaiseDisturbed);
            }
        }

        public void Disable()
        {
            if (!_isEnabled) return;
            _isEnabled = false;

            foreach (var bone in _bones)
            {
                bone.UnsubscribeDisturbed(RaiseDisturbed);
                bone.RagdollDisable(_ragdollConfig);
            }
        }

        private void RaiseDisturbed() => Disturbed?.Invoke();
    }
}

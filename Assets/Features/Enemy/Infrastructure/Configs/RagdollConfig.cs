using Features.Interaction.Infrastructure.Configs;
using UnityEngine;

namespace Features.Enemy.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "RagdollConfig", menuName = "Configs/Enemy/RagdollConfig")]
    public sealed class RagdollConfig : ScriptableObject
    {
        [SerializeField] private InteractableConfig _interactableConfig;
        [SerializeField] private InteractablePhysicsConfig _interactablePhysicsConfig;

        public InteractableConfig InteractableConfig => _interactableConfig;
        public PhysicsMaterial DefaultMaterial => _interactablePhysicsConfig.DefaultMaterial;
        public PhysicsMaterial RagdollMaterial => _interactablePhysicsConfig.InteractMaterial;

        private void OnValidate()
        {
            if (_interactableConfig != null && _interactablePhysicsConfig != null &&
                _interactableConfig.InteractablePhysicsConfig != _interactablePhysicsConfig)
            {
                Debug.LogWarning(
                    $"{name}: physics config differs from the one inside {_interactableConfig.name}.", this);
            }
        }
    }
}

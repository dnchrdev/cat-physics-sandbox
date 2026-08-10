using UnityEngine;

namespace Feature.PlayerFeature
{
    [CreateAssetMenu(fileName = "PlayerCharacterConfig", menuName = "Configs/PlayerCharacter")]
    public class PlayerCharacterConfig : ScriptableObject
    {
        [Header("Jumping")] [field: SerializeField]
        public float JumpSpeed = 2f;

        [field: SerializeField] public float CoyoteTime = 0.35f;

        [Header("Sliding")] [field: SerializeField]
        public float SlideFriction = 0.8f;

        [field: SerializeField] public float SlideGravity = 9.8f;

        [Header("Gravity")] [field: SerializeField]
        public float Gravity = 9.8f;
    }
}
using UnityEngine;

namespace Feature.PlayerFeature
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "SO/ChaacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [Header("Jumping")] [field: SerializeField]
        public float JumpSpeed = 20f;

        [field: SerializeField] public float CoyoteTime = 0.35f;

        [Header("Sliding")] [field: SerializeField]
        public float SlideFriction = 0.8f;

        [field: SerializeField] public float SlideGravity = 20f;

        [Header("Gravity")] [field: SerializeField]
        public float Gravity = 20f;
    }
}
using Features.Player.Entities;
using UnityEngine;

namespace Features.Player.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "PlayerCharacterConfig", menuName = "Configs/PlayerCharacter")]
    public class PlayerCharacterConfig : ScriptableObject
    {
        [Header("Jumping")]
        [SerializeField] private float _jumpSpeed = 2f;
        [SerializeField] private float _coyoteTime = 0.35f;

        [Header("Sliding")]
        [SerializeField] private float _slideFriction = 0.8f;
        [SerializeField] private float _slideGravity = 9.8f;

        [Header("Gravity")]
        [SerializeField] private float _gravity = 9.8f;

        public MovementRules ToRules() =>
            new MovementRules(_jumpSpeed, _coyoteTime, _slideFriction, _slideGravity, _gravity);
    }
}

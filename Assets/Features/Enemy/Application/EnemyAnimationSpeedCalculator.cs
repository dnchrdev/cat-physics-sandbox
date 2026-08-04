using System;
using Feature.Core;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyAnimationSpeedCalculator: IInitializable
    {
        [Inject] private readonly EnemyConfig _enemyConfig;
        [Inject] private readonly IMovableEnemy _movableEnemy;
        
        private Vector3 _prevPosition;
        private float _prevWalkSpeed;
        
        public void Initialize()
        {
            Reset();
        }

        public void Reset()
        {
            _prevPosition = _movableEnemy.GetPosition();
            _prevWalkSpeed = 0f;
        }

        public float GetWalkAnimationSpeed(float maxSpeed, float maxAnimationSpeed, float dt)
        {
            var currentPosition = _movableEnemy.GetPosition();

            var velocity = Vector3.ProjectOnPlane(_prevPosition - currentPosition, Vector3.up).magnitude / dt;
            var walkSpeed = AdditionalMath.Map(velocity, 0f, maxSpeed, 0f, maxAnimationSpeed);

            var validPrevWalkSpeed = float.IsNaN(_prevWalkSpeed) ? 0f: _prevWalkSpeed;

            walkSpeed = Mathf.Lerp(validPrevWalkSpeed, walkSpeed, 1f - Mathf.Exp(-_enemyConfig.WalkAnimationResponse * dt));

            _prevPosition = currentPosition;
            _prevWalkSpeed = walkSpeed;

            return walkSpeed;
        }
    }
}
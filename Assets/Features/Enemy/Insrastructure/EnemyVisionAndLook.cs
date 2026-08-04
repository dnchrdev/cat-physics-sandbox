using Feature.PlayerFeature;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyVisionAndLook
    {
        public float HeadLookWeight { get; private set; }

        [Inject] private readonly EnemyRig _enemyRig;
        [Inject] private readonly IMovableEnemy _movableEnemy;
        [Inject] private readonly EnemyConfig _config;
        [Inject] private readonly IReadOnlyPlayer _readOnlyPlayer;
        
        private bool _isHeadLooking;

        public void Tick(float dt)
        {
             UpdateHeadLookToPlayer(dt);
        }

        public bool IsPlayerVisible()
        {
            if(Physics.Linecast(_enemyRig.Head.position, _readOnlyPlayer.Position, out var hitInfo, _config.VisionMask))
            {
                return false;
            }
            return true;
        }

        public void EnableHeadLooking() => _isHeadLooking = true;
        public void DisableHeadLooking() => _isHeadLooking = false;

        private void UpdateHeadLookToPlayer(float dt)
        {
            var directionToPlayer = _readOnlyPlayer.Position - _enemyRig.Head.position;

            var directionFlat = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up).normalized;
            var agentFlat = Vector3.ProjectOnPlane(_movableEnemy.GetForward(), Vector3.up);
            var yawAngle = Vector3.Angle(agentFlat, directionFlat);

            var pitchAngle = Mathf.Abs(Vector3.Angle(directionFlat, directionToPlayer));

            bool withinAngle = yawAngle <= _config.HeadLookYawMaxAngle && pitchAngle <= _config.HeadLookPitchMaxAngle;

            float targetWeight = (withinAngle && _isHeadLooking && IsPlayerVisible()) ? 1f : 0f;

            HeadLookWeight = Mathf.Lerp(
                HeadLookWeight,
                targetWeight,
                1f - Mathf.Exp(-_config.HeadLookResponse * dt));
        }


    }
}
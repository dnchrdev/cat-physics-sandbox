using Feature.PlayerFeature;
using Feature.Shared;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Feature.EnemyFeature
{
    public class EnemyAttackAbility : IInitializable
    {
        [Inject] private readonly IAnimatableEnemy _animatableEnemy;
        [Inject] private readonly IMovableEnemy _movableEnemy;
        [Inject] private readonly EnemyRig _enemyRig;
        [Inject] private readonly EnemyConfig _config;
        [Inject] private readonly Enemy _enemy;
        [Inject] private readonly IReadOnlyPlayer _readOnlyPlayer;
        [Inject] private readonly Player _targetPlayer;

        private float _cooldown;

        public bool IsPlaying { get; private set; }

        public void Initialize()
        {
            IsPlaying = false;
            _cooldown = -1f;
        }

        public bool CanAttack =>
            !IsPlaying &&
            _cooldown <= 0f;

        public void Tick(float dt)
        {
            _cooldown -= dt;
        }

        public void Start()
        {
            IsPlaying = true;
        }

        public void Attack()
        {
            if (!CanAttack)
                return;

            _cooldown = _config.AttackCooldown;
            _animatableEnemy.ResetSttackStop();
            _animatableEnemy.AttackStart();
        }

        public bool IsAttackAngleValid(float maxAngle)
        {
            var directionToPlayer =
                Vector3.ProjectOnPlane(_readOnlyPlayer.Position - _movableEnemy.GetPosition(), Vector3.up).normalized;

            var angle = Vector3.Angle(directionToPlayer, _movableEnemy.GetForward());

            if (angle <= maxAngle)
            {
                return true;
            }

            return false;
        }

        public void UpdateAttackRotation(float dt)
        {
            var targetDirection = Vector3
                .ProjectOnPlane(_readOnlyPlayer.Position - _movableEnemy.GetPosition(), Vector3.up).normalized;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            _movableEnemy.SetRotation(Quaternion.RotateTowards(
                _movableEnemy.GetRotation(),
                targetRotation,
                _config.AttackRotationSpeed * dt)
            );
        }

        public void Finish()
        {
            IsPlaying = false;
        }

        public void Cancel()
        {
            IsPlaying = false;
            _animatableEnemy.AttackStop();
            _animatableEnemy.ResetAttack();
            _animatableEnemy.PlayIdleWalk();
        }

        public void ApplyDamage()
        {
            if (_enemy.IsAlive == false)
                return;

            if (CanHitTarget() && IsAttackAngleValid(_config.ApplyDamageMaxYawAngle))
            {
                var attackInfo = new AttackInfo(_enemy, false);
                _targetPlayer.RecieveHit(attackInfo);
            }
        }

        public bool CanHitTarget()
        {
            if (Vector3.Dot(_readOnlyPlayer.Position - _enemyRig.Head.position, Vector3.up) >= 0f)
            {
                if (Vector3.Distance(_enemyRig.Head.position, _readOnlyPlayer.Position) <= _config.AttackSphereDistance)
                {
                    return true;
                }
            }
            else
            {
                var horizontalDelta =
                    Vector3.ProjectOnPlane(_enemyRig.Head.position - _readOnlyPlayer.Position, Vector3.up);
                if (horizontalDelta.magnitude <= _config.AttackHorizontalDistance)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
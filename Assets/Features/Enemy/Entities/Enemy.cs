using System;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.Utilities;

namespace Features.Enemy.Entities
{
    public sealed class Enemy : IEntity, ITarget
    {
        public event Action<AttackInfo> HitReceived;

        private float _angerTimeLeft;

        public EnemyMode Mode { get; private set; } = EnemyMode.Patrolling;
        public TeamType Team => TeamType.Enemy;
        public bool IsAlive => Mode != EnemyMode.KnockedOut;
        public bool IsAngry => Mode == EnemyMode.Pursuing;
        public bool IsKnockedOut => Mode == EnemyMode.KnockedOut;

        public IEntity TargetPlayer { get; private set; }

        public float TimeUndisturbed { get; private set; }

        public Result ReceiveHit(AttackInfo attackInfo)
        {
            if (!IsAlive)
                return Result.Failure("Enemy is already knocked out");
            if (attackInfo.Entity.Team == TeamType.None || attackInfo.Entity.Team == Team)
                return Result.Failure("Same team");

            TargetPlayer = attackInfo.Entity;
            HitReceived?.Invoke(attackInfo);
            return Result.Success();
        }

        public void Tick(float dt)
        {
            switch (Mode)
            {
                case EnemyMode.Pursuing:
                    _angerTimeLeft -= dt;
                    if (_angerTimeLeft <= 0f)
                        Mode = EnemyMode.Patrolling;
                    break;
                case EnemyMode.KnockedOut:
                    TimeUndisturbed += dt;
                    break;
            }
        }

        public void Knockout()
        {
            Mode = EnemyMode.KnockedOut;
            _angerTimeLeft = 0f;
            TimeUndisturbed = 0f;
        }

        public void Disturb()
        {
            if (IsKnockedOut)
                TimeUndisturbed = 0f;
        }

        public bool CanRecover(float restDuration, bool isHeld) =>
            IsKnockedOut && TimeUndisturbed > restDuration && !isHeld;

        public void Recover(float angerDuration)
        {
            Mode = EnemyMode.Pursuing;
            _angerTimeLeft = angerDuration;
        }

        public void CalmDown()
        {
            if (IsAngry)
                Mode = EnemyMode.Patrolling;
        }
    }
}

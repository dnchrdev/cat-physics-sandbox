using System;
using Features.Enemy.InterfaceAdapters.Ports;
using UnityEngine;

namespace Features.Enemy.Infrastructure.Rig
{
    public class EnemyAnimationEvents : MonoBehaviour, IEnemyAnimationEvents
    {
        public event Action ApplyDamageEvent;
        public event Action AttackStartedEvent;
        public event Action AttackFinishedEvent;

        public void AttackApplyDamage() => ApplyDamageEvent?.Invoke();
        public void AttackStarted() => AttackStartedEvent?.Invoke();
        public void AttackFinished() => AttackFinishedEvent?.Invoke();
    }
}

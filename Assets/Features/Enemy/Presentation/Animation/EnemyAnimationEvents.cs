using System;
using UnityEngine;

namespace Features.Enemy.Presentation.Animation
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        public event Action ApplyDamageEvent;
        public event Action AttackStartedEvent;
        public event Action AttackFinishedEvent;

        public void AttackApplyDamage()
        {
            ApplyDamageEvent?.Invoke();
        }
        public void AttackStarted()
        {
            AttackStartedEvent?.Invoke();
        }
        public void AttackFinished()
        {
            AttackFinishedEvent?.Invoke();
        }
    }
}

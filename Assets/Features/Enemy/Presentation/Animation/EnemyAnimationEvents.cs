using Feature.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

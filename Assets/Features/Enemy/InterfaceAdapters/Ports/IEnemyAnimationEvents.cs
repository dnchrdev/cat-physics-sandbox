using System;

namespace Features.Enemy.InterfaceAdapters.Ports
{
    public interface IEnemyAnimationEvents
    {
        event Action ApplyDamageEvent;
        event Action AttackStartedEvent;
        event Action AttackFinishedEvent;
    }
}

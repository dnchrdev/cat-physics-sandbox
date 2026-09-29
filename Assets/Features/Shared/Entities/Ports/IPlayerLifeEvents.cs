using System;

namespace Features.Shared.Entities.Ports
{

    public interface IPlayerLifeEvents
    {
        event Action<AttackInfo> HitReceived;
        event Action Knockedout;
        event Action Continued;
        event Action Respawned;
    }
}

using System;

namespace Features.Shared
{
    public interface ILiveEvents
    {
        event Action<AttackInfo> HitRecieved;
        event Action Knockouted;
        event Action Continiued;
    }
}
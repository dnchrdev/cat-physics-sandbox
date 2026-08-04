using System;

namespace Feature.Shared
{
    public interface ILiveEvents
    {
        event Action<AttackInfo> HitRecieved;
        event Action Knockouted;
        event Action Continiued;
    }
}
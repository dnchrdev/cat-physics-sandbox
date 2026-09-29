using System;

namespace Features.Enemy.InterfaceAdapters.Ports
{
    public interface IRagdollEvents
    {
        event Action Disturbed;
    }
}

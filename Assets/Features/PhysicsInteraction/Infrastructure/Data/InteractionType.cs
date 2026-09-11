using System;

namespace Features.PhysicsInteraction.Infrastructure.Data
{
    [Serializable]
    public enum InteractionType
    {
        None,
        Full,
        Hit,
        Grab
    }
}
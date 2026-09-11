using System;

namespace Features.PhysicsInteraction.Infrastructure.Data
{
    [Serializable]
    public enum OutlineMode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }
}
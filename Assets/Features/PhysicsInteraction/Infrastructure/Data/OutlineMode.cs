using System;

namespace Feature.PhysicsInteraction
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
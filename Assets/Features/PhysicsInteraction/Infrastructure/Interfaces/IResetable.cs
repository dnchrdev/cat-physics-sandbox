using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IResetable
    {
        void ResetState();
        void SaveState();
    }
}
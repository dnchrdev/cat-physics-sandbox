using UnityEngine;

namespace Feature.PlayerFeature
{
    public interface IReadOnlyPlayer
    {
        bool IsAlive { get; }
        Vector3 Position { get; }
    }
}
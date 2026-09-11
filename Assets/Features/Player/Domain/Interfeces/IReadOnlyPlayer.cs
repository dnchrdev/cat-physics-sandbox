using UnityEngine;

namespace Features.Player.Domain.Interfeces
{
    public interface IReadOnlyPlayer
    {
        bool IsAlive { get; }
        Vector3 Position { get; }
    }
}
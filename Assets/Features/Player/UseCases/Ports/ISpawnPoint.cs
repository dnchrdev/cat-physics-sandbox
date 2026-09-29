using UnityEngine;

namespace Features.Player.UseCases.Ports
{
    public interface ISpawnPoint
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}

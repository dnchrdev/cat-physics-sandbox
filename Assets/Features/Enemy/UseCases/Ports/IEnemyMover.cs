using UnityEngine;

namespace Features.Enemy.UseCases.Ports
{
    public interface IEnemyMover
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Quaternion Rotation { get; }

        Vector3 Destination { get; }

        void MoveTo(Vector3 destination, float speed);
        void Stop();
        void SetRotation(Quaternion rotation);
        void Teleport(Vector3 position);
    }
}

using UnityEngine;

namespace Feature.EnemyFeature
{
    public interface IMovableEnemy
    {
        void Enable();
        void Disable();
        Vector3 GetForward();
        Vector3 GetPosition();
        void SetPosition(Vector3 newPosition);
        Quaternion GetRotation();
        void SetRotation(Quaternion newRotation);
        void SetDestination(Vector3 destination);
        Vector3 GetDestination();
        void SetSpeed(float speed);
    }
}
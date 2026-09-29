using UnityEngine;

namespace Features.Interaction.Entities.Ports
{
    public interface IInteractableBody
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        bool IsKinematic { set; }
        
        void SetHeldMode(bool held);
        
        void ResetMotion(bool isKinematic);

        void SetPose(Vector3 position, Quaternion rotation);
        void AddImpulse(Vector3 impulse);
        void AddImpulseAtPoint(Vector3 impulse, Vector3 point);

        Vector3 ClosestPoint(Vector3 from);
        bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, out Vector3 point);
    }
}

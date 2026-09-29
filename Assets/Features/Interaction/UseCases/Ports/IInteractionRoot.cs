using UnityEngine;

namespace Features.Interaction.UseCases.Ports
{
    public interface IInteractionRoot
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        void SetVelocity(Vector3 velocity);
        void SetAngularVelocity(Vector3 angularVelocity);
    }
}

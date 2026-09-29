using UnityEngine;

namespace Features.Camera.UseCases.Ports
{
    public interface ICameraMover
    {
        Vector3 PositionRootPosition { get; }
        Quaternion RotationRootRotation { get; }
        Vector3 RotationRootForward { get; }
        Vector3 RotationRootRight { get; }

        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        void SetBobOffset(Vector3 localOffset);
        void SetLeanRotation(Quaternion localRotation);
        void Reset(Vector3 newPosition);
    }
}

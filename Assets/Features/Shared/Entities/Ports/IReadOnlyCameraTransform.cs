using UnityEngine;

namespace Features.Shared.Entities.Ports
{
    public interface IReadOnlyCameraTransform
    {
        Vector3 Forward { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}

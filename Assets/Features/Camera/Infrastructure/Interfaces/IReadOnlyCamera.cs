using UnityEngine;

namespace Features.Camera.Infrastructure.Interfaces
{
    public interface IReadOnlyCamera
    {
        UnityEngine.Camera Camera{ get; }
        Vector3 Forward { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}
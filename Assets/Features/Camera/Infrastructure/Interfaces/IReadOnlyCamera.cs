using System.Collections;
using UnityEngine;

namespace Feature.CameraFeature
{
    public interface IReadOnlyCamera
    {
        Camera Camera{ get; }
        Vector3 Forward { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}
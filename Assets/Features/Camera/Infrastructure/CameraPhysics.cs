using Features.Camera.Infrastructure.Interfaces;
using UnityEngine;

namespace Features.Camera.Infrastructure
{
    public class CameraPhysics : MonoBehaviour, IReadOnlyCamera, ICameraPhysics
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;
        public Vector3 Forward => _camera.transform.forward;
        public Vector3 Position => _camera.transform.position;
        public Quaternion Rotation => _camera.transform.rotation;

        public void ApplyFOV(float fov)
        {
            _camera.fieldOfView = fov;
        }

    }
}
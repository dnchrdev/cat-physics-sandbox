using UnityEngine;

namespace Feature.CameraFeature
{
    public class CameraPhysics : MonoBehaviour, IReadOnlyCamera, ICameraPhysics
    {
        [SerializeField] private Camera _camera;

        public Camera Camera => _camera;
        public Vector3 Forward => _camera.transform.forward;
        public Vector3 Position => _camera.transform.position;
        public Quaternion Rotation => _camera.transform.rotation;

        public void ApplyFOV(float fov)
        {
            _camera.fieldOfView = fov;
        }

    }
}
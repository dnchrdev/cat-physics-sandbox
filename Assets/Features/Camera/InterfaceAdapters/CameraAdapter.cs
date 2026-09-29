using Features.Shared.Entities.Ports;
using Features.Shared.Infrastructure;
using UnityEngine;

namespace Features.Camera.InterfaceAdapters
{
    public class CameraAdapter : IReadOnlyCameraTransform
    {
        private readonly IReadOnlyCameraPhysical _cameraPhysical;

        public CameraAdapter(IReadOnlyCameraPhysical cameraPhysical)
        {
            _cameraPhysical = cameraPhysical;
        }
        
        public Vector3 Forward => _cameraPhysical.Camera.transform.forward;
        public Vector3 Position => _cameraPhysical.Camera.transform.position;
        public Quaternion Rotation => _cameraPhysical.Camera.transform.rotation;
    }
}

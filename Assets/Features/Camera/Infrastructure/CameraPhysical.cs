using Features.Shared.Infrastructure;
using UnityEngine;

namespace Features.Camera.Infrastructure
{
    public class CameraPhysical : MonoBehaviour, IReadOnlyCameraPhysical
    {
        [SerializeField] private UnityEngine.Camera _camera;
 
        public UnityEngine.Camera Camera => _camera;
    }
}
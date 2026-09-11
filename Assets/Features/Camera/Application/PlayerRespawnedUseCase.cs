using Features.Camera.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.Camera.Application
{
    public class PlayerRespawnedUseCase
    {
        [Inject] private CameraRig _cameraRig;

        public void PlayerRespawned(Vector3 newPos)
        {
            _cameraRig.PositionRoot.position = newPos;
            _cameraRig.PositionRoot.rotation = Quaternion.identity;

            _cameraRig.RotationRoot.localPosition = Vector3.zero;
            _cameraRig.RotationRoot.localRotation = Quaternion.identity;

            _cameraRig.SpringRoot.localPosition = Vector3.zero;
            _cameraRig.SpringRoot.localRotation = Quaternion.identity;

            _cameraRig.LeanRoot.localPosition = Vector3.zero;
            _cameraRig.LeanRoot.localRotation = Quaternion.identity;
        }

        
    }
}
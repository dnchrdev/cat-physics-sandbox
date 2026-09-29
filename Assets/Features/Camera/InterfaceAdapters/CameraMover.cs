using Features.Camera.Infrastructure;
using Features.Camera.UseCases.Ports;
using UnityEngine;

namespace Features.Camera.InterfaceAdapters
{
    public sealed class CameraMover : ICameraMover
    {
        private readonly Transform _positionRoot;
        private readonly Transform _rotationRoot;
        private readonly Transform _bobRoot;
        private readonly Transform _springRoot;
        private readonly Transform _leanRoot;

        public CameraMover(CameraRefs cameraRigView)
        {
            _positionRoot = cameraRigView.PositionRoot;
            _rotationRoot = cameraRigView.RotationRoot;
            _bobRoot = cameraRigView.BobRoot;
            _springRoot = cameraRigView.SpringRoot;
            _leanRoot = cameraRigView.LeanRoot;
        }

        public Vector3 PositionRootPosition => _positionRoot.position;
        public Quaternion RotationRootRotation => _rotationRoot.rotation;
        public Vector3 RotationRootForward => _rotationRoot.forward;
        public Vector3 RotationRootRight => _rotationRoot.right;

        public void SetPosition(Vector3 position)
        {
            _positionRoot.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _rotationRoot.rotation = rotation;
        }

        public void SetBobOffset(Vector3 localOffset)
        {
            _bobRoot.localPosition = localOffset;
        }

        public void SetLeanRotation(Quaternion localRotation)
        {
            _leanRoot.localRotation = localRotation;
        }

        public void Reset(Vector3 newPosition)
        {
            _positionRoot.position = newPosition;
            _positionRoot.rotation = Quaternion.identity;

            _rotationRoot.localPosition = Vector3.zero;
            _rotationRoot.localRotation = Quaternion.identity;

            _springRoot.localPosition = Vector3.zero;
            _springRoot.localRotation = Quaternion.identity;

            _leanRoot.localPosition = Vector3.zero;
            _leanRoot.localRotation = Quaternion.identity;
        }
    }
}

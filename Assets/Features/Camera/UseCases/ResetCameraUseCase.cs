using Features.Camera.UseCases.Ports;
using UnityEngine;

namespace Features.Camera.UseCases
{
    public class ResetCameraUseCase
    {
        private readonly ICameraMover _mover;

        public ResetCameraUseCase(ICameraMover mover)
        {
            _mover = mover;
        }

        public void Execute(Vector3 newPosition)
        {
            _mover.Reset(newPosition);
        }
    }
}

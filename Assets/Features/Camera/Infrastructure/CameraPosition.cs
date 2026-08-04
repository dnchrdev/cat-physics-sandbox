using Feature.PlayerFeature;
using UnityEngine;
using Zenject;

namespace Feature.CameraFeature
{
    public class CameraPosition
    {
        [Inject] private readonly CameraConfig _config;
        [Inject] private readonly Player _player;
        
        private readonly Transform _root;

        public CameraPosition(CameraRig cameraRig)
        {
            _root = cameraRig.PositionRoot;
        }

        public void Tick(float dt)
        {
            Vector3 current = _root.position;
            Vector3 target = _player.Position;

            _root.position = Vector3.Lerp(current, target, 1f);
            //_root.position = Vector3.Lerp(current, target,  - Mathf.Exp(-_config.MoveSpeedSmoothing * dt));
        }

    }
}
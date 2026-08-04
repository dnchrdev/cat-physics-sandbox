using Feature.Core;
using Feature.EnemyFeature;
using Feature.PlayerFeature;
using Feature.Shared;
using UnityEngine;
using Zenject;

namespace Feature.CameraFeature
{
    public class PlayerDeadCameraRotation
    {
        [Inject] private readonly CameraConfig _config;
        [Inject] private readonly IWorldEntityService _worldEntityService;
        
        private readonly Transform _root;
        private readonly Transform _positionRoot;
        
        private Transform _enemyHead;

        public PlayerDeadCameraRotation(CameraRig cameraRig)
        {
            _root = cameraRig.RotationRoot;
            _positionRoot = cameraRig.PositionRoot;
        }

        public void UpdateFollowedEnemyHead(AttackInfo  info)
        {
            _enemyHead = _worldEntityService.GetObjectByEntity(info.Entity).transform;
        }
        
        public void Tick(float dt)
        {
            var directionFollow = _root.transform.forward;

            if (_enemyHead != null)
                directionFollow = _enemyHead.position - _positionRoot.position;

            Vector3 euler = Quaternion.LookRotation(directionFollow, Vector3.up).eulerAngles;
            Quaternion targetRot = Quaternion.Euler(euler.x, euler.y, 0f);
            _root.rotation = Quaternion.Slerp(_root.rotation, targetRot, 1f - Mathf.Exp(-_config.DeadPlayerTurnSpeedResponse * dt));
        }


    }
}
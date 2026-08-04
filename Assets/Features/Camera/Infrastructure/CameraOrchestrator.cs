using Feature.Core;
using Feature.EnemyFeature;
using Feature.Input;
using Feature.PlayerFeature;
using Feature.Storage;
using System;
using UnityEngine;
using Zenject;

namespace Feature.CameraFeature
{
    public class CameraOrchestrator : IInitializable, IDisposable, ITickable
    {
        [Inject] private readonly CameraPosition _cameraPosition;
        [Inject] private readonly CameraRotation _cameraRotation;
        [Inject] private readonly CameraLean _cameraLean;
        [Inject] private readonly PlayerDeadCameraRotation _playerDeadCameraRotation;
        [Inject] private readonly PlayerRespawnedUseCase _playerRespawnedUseCase;
        [Inject] private readonly CameraHeadbob _cameraHeadbob;
        [Inject] private readonly ICameraInput _cameraInput;
        [Inject] private readonly Player _player;
        [Inject] private readonly IGamePauseService _pauseService;

        public void Initialize()
        {
            _cameraInput.LookEvent += UpdateCameraRotation;
            _player.Respawned += HandlePlayerRespawn;
            _player.Continiued += HandlePlayerRespawn;
            _player.HitRecieved += _playerDeadCameraRotation.UpdateFollowedEnemyHead;
        }

        public void Dispose()
        {
            _cameraInput.LookEvent -= UpdateCameraRotation;
            _player.Respawned -= HandlePlayerRespawn;
            _player.Continiued -= HandlePlayerRespawn;
            _player.HitRecieved -= _playerDeadCameraRotation.UpdateFollowedEnemyHead;
        }

        public void Tick()
        {
            if (_player.IsAlive)
            {
                _cameraPosition.Tick(Time.deltaTime);
                _cameraLean.Tick(Time.deltaTime);
                _cameraHeadbob.Tick(Time.deltaTime);
            }
            else
            {
                _playerDeadCameraRotation.Tick(Time.deltaTime);
            }
        }

        private void UpdateCameraRotation(Vector2 lookDelta)
        {
            if (_player.IsAlive && _pauseService.Paused == false)
               _cameraRotation.Tick(lookDelta, Time.deltaTime);
        }
        
        private void HandlePlayerRespawn()
        {
            _playerRespawnedUseCase.PlayerRespawned(_player.Position);
        }
    }
}
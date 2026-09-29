using System;
using Features.Camera.UseCases;
using Features.Core.UseCases;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases.Input;
using UnityEngine;
using Zenject;

namespace Features.Camera.InterfaceAdapters
{
    public sealed class CameraController : IInitializable, IDisposable, ITickable
    {
        private readonly CameraFollowUseCase _follow;
        private readonly CameraRotationUseCase _rotation;
        private readonly CameraLeanUseCase _lean;
        private readonly CameraHeadbobUseCase _headbob;
        private readonly FollowEnemyHeadUseCase _followEnemyHead;
        private readonly ResetCameraUseCase _reset;
        private readonly ICameraInput _input;
        private readonly IPlayerLifeEvents _playerLifeEvents;
        private readonly IReadOnlyPlayer _player;
        private readonly GamePauseService _pause;

        public CameraController(
            CameraFollowUseCase follow,
            CameraRotationUseCase rotation,
            CameraLeanUseCase lean,
            CameraHeadbobUseCase headbob,
            FollowEnemyHeadUseCase followEnemyHead,
            ResetCameraUseCase reset,
            ICameraInput input,
            IPlayerLifeEvents playerLifeEvents,
            IReadOnlyPlayer player,
            GamePauseService pause)
        {
            _follow = follow;
            _rotation = rotation;
            _lean = lean;
            _headbob = headbob;
            _followEnemyHead = followEnemyHead;
            _reset = reset;
            _input = input;
            _playerLifeEvents = playerLifeEvents;
            _player = player;
            _pause = pause;
        }

        public void Initialize()
        {
            _input.LookEvent += OnLook;
            _playerLifeEvents.Respawned += OnPlayerRespawned;
            _playerLifeEvents.Continued += OnPlayerRespawned;
            _playerLifeEvents.HitReceived += _followEnemyHead.UpdateFollowedEnemy;
        }

        public void Dispose()
        {
            _input.LookEvent -= OnLook;
            _playerLifeEvents.Respawned -= OnPlayerRespawned;
            _playerLifeEvents.Continued -= OnPlayerRespawned;
            _playerLifeEvents.HitReceived -= _followEnemyHead.UpdateFollowedEnemy;
        }

        public void Tick()
        {
            var dt = Time.deltaTime;

            if (_player.IsAlive)
            {
                _follow.Tick(dt);
                _lean.Tick(dt);
                _headbob.Tick(dt);
            }
            else
            {
                _followEnemyHead.Tick(dt);
            }
        }

        private void OnLook(Vector2 lookDelta)
        {
            if (_player.IsAlive && !_pause.IsPaused)
                _rotation.Tick(lookDelta, Time.deltaTime);
        }

        private void OnPlayerRespawned() => _reset.Execute(_player.Position);
    }
}

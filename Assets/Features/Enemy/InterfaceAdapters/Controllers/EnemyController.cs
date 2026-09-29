using System;
using Features.Core.UseCases;
using Features.Enemy.InterfaceAdapters.Ports;
using Features.Enemy.UseCases;
using Features.Shared.Entities.Ports;
using UnityEngine;
using Zenject;

namespace Features.Enemy.InterfaceAdapters.Controllers
{
    public sealed class EnemyController : IInitializable, IDisposable, ITickable
    {
        private readonly GamePauseService _pause;
        private readonly Entities.Enemy _enemy;
        private readonly IPlayerLifeEvents _playerLifeEvents;
        private readonly IEnemyAnimationEvents _animationEvents;
        private readonly IRagdollEvents _ragdollEvents;
        private readonly EnemyUpdateUseCase _update;
        private readonly EnemyHeadLookUseCase _headLook;
        private readonly EnemyLifecycleUseCase _lifecycle;
        private readonly EnemyAttackUseCase _attack;
        private readonly WalkAnimationUseCase _walkAnimation;

        public EnemyController(
            GamePauseService pause,
            Entities.Enemy enemy,
            IPlayerLifeEvents playerLifeEvents,
            IEnemyAnimationEvents animationEvents,
            IRagdollEvents ragdollEvents,
            EnemyUpdateUseCase update,
            EnemyHeadLookUseCase headLook,
            EnemyLifecycleUseCase lifecycle,
            EnemyAttackUseCase attack,
            WalkAnimationUseCase walkAnimation)
        {
            _pause = pause;
            _enemy = enemy;
            _playerLifeEvents = playerLifeEvents;
            _animationEvents = animationEvents;
            _ragdollEvents = ragdollEvents;
            _update = update;
            _headLook = headLook;
            _lifecycle = lifecycle;
            _attack = attack;
            _walkAnimation = walkAnimation;
        }

        public void Initialize()
        {
            _walkAnimation.Reset();

            _enemy.HitReceived += _lifecycle.HandleHit;
            _ragdollEvents.Disturbed += _lifecycle.HandleRagdollDisturbed;
            _playerLifeEvents.Respawned += _lifecycle.HandlePlayerRespawned;
            _playerLifeEvents.Continued += _lifecycle.HandlePlayerRespawned;
            _animationEvents.AttackStartedEvent += _attack.HandleSwingStarted;
            _animationEvents.ApplyDamageEvent += _attack.HandleDamageFrame;
            _animationEvents.AttackFinishedEvent += _attack.HandleSwingFinished;
        }

        public void Dispose()
        {
            _enemy.HitReceived -= _lifecycle.HandleHit;
            _ragdollEvents.Disturbed -= _lifecycle.HandleRagdollDisturbed;
            _playerLifeEvents.Respawned -= _lifecycle.HandlePlayerRespawned;
            _playerLifeEvents.Continued -= _lifecycle.HandlePlayerRespawned;
            _animationEvents.AttackStartedEvent -= _attack.HandleSwingStarted;
            _animationEvents.ApplyDamageEvent -= _attack.HandleDamageFrame;
            _animationEvents.AttackFinishedEvent -= _attack.HandleSwingFinished;
        }

        public void Tick()
        {
            var dt = Time.deltaTime;

            if (!_pause.IsPaused)
                _update.Tick(dt);

            _headLook.Tick(dt);
        }
    }
}

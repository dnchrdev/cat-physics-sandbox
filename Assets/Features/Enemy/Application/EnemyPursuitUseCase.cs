using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyPursuitUseCase
    {
        [Inject] private Blackboard _bb;
        [Inject] private EnemyRig _enemyRig;
        [Inject] private IMovableEnemy _movableEnemy;
        [Inject] private PatrolPoints _patrolPoints;
        [Inject] private RagdollController _ragdollController;
        [Inject]private readonly IAnimatableEnemy _animatableEnemy;
        [Inject] private EnemyConfig _config;
        [Inject] private EnemyAnimationSpeedCalculator _enemyAnimationSpeedCalculator;

        private CancellationTokenSource _pursuitCancel;

        public void StopPursuiting()
        {
            _pursuitCancel?.Cancel();
            _pursuitCancel?.Dispose();
            _pursuitCancel = null;

            _bb.IsAngry = false;
        }

        public void Respawned()
        {
            _movableEnemy.SetPosition(_patrolPoints.GetPatrolPointPosition(_patrolPoints.GetNearestPatrolIndex(_enemyRig.Head.position)));
            _enemyAnimationSpeedCalculator.Reset();
            StartPursuit(_config.PursuitDuration);
        }

        private void StartPursuit(float duration)
        {
            StopPursuiting();
            _pursuitCancel = new CancellationTokenSource();
            _bb.IsAngry = true;
            _bb.IsRagdoll = false;

            _ragdollController.DisableRagdoll();
            _animatableEnemy.Enable();  

            PursuitDurationAsync(duration, _pursuitCancel.Token).Forget();
        }

        private async UniTask PursuitDurationAsync(float duration, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(duration),
                    cancellationToken: token);
                _bb.IsAngry = false;
            }
            catch (OperationCanceledException)
            {
                // Persuit was canceled 
            }
        }
    }
}
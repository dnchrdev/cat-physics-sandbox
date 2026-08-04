using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyKnockoutedUseCase
    {
        [Inject] private readonly Blackboard _bb;
        [Inject] private readonly Enemy _enemy;
        [Inject]private readonly IAnimatableEnemy _animatableEnemy;
        [Inject] private readonly RagdollController _ragdollController;
        [Inject] private readonly EnemyConfig _enemyConfig;

        private CancellationTokenSource _respawnCancel;

        public void Knockouted()
        {
            _respawnCancel?.Cancel();
            _respawnCancel?.Dispose();
            _respawnCancel = new CancellationTokenSource();

            _bb.IsAngry = false;
            _bb.IsRagdoll = true;
            _animatableEnemy.Disable();
            _ragdollController.EnableRagdoll();

            RespawningDurationAsync(_respawnCancel.Token).Forget();
        }

        private async UniTask RespawningDurationAsync(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    var waitTime = Mathf.Clamp(
                        _enemyConfig.RespawnDuration - _ragdollController.TimerSinceLastInteraction,
                        0f,
                        _enemyConfig.RespawnDuration);

                    await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: token);

                    var stillSettling = _ragdollController.TimerSinceLastInteraction <= _enemyConfig.RespawnDuration
                        || _ragdollController.IsAnyBoneGrabbed();

                    if (!stillSettling)
                        break;
                }

                _enemy.Respawn();
            }
            catch (OperationCanceledException)
            {
                // Respawn was cancelled
            }
        }
    }
}
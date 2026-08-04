using Feature.Shared;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyUseCaseOrchestrator
    {
        [Inject] private readonly EnemyConfig _enemyConfig;
        [Inject] private readonly EnemyKnockoutedUseCase _knockoutedUseCase;
        [Inject] private readonly EnemyPursuitUseCase _pursuitUseCase;
        [Inject] private readonly EnemyAttackAbility _attackAbillity;

        public void HandleOnEnemyRespawned()
        {
            _attackAbillity.Cancel();
            _pursuitUseCase.Respawned();
        }

        public void HandleOnHitRecieved(AttackInfo attackInfo)
        {
            //Debug.Log($"atackVElocity = {attackInfo.HitVelocity}");
            if (attackInfo.HitVelocity < _enemyConfig.HitMinVelocity) return;

            _attackAbillity.Cancel();
            _pursuitUseCase.StopPursuiting();
            _knockoutedUseCase.Knockouted();
        }

        public void OnPlayerRespawned()
        {
            _attackAbillity.Cancel();
            _pursuitUseCase.StopPursuiting();
        }

    }
}
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases
{
    public sealed class EnemyUpdateUseCase
    {
        private readonly EnemyLifecycleUseCase _lifecycle;
        private readonly EnemyAttackUseCase _attack;
        private readonly WalkAnimationUseCase _walkAnimation;
        private readonly IEnemyBehaviour _behaviour;

        public EnemyUpdateUseCase(
            EnemyLifecycleUseCase lifecycle,
            EnemyAttackUseCase attack,
            WalkAnimationUseCase walkAnimation,
            IEnemyBehaviour behaviour)
        {
            _lifecycle = lifecycle;
            _attack = attack;
            _walkAnimation = walkAnimation;
            _behaviour = behaviour;
        }

        public void Tick(float dt)
        {
            _lifecycle.Tick(dt);
            _attack.Tick(dt);
            _walkAnimation.Update(dt);
            _behaviour.Evaluate(dt);
        }
    }
}

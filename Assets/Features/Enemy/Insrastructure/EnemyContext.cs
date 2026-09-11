using Features.Enemy.Application;
using Features.Enemy.Insrastructure.Configs;
using Zenject;

namespace Features.Enemy.Insrastructure
{
    public class EnemyContext
    {
        [Inject] public readonly Domain.Enemy Enemy;
        [Inject] public readonly Player.Domain.Player Player;
        [Inject] public readonly EnemyRig EnemyRig;
        [Inject] public readonly IAnimatableEnemy AnimatableEnemy;
        [Inject] public readonly IMovableEnemy MovableEnemy;
        [Inject] public readonly EnemyVisionAndLook EnemyVisionAndLook;
        [Inject] public readonly EnemyConfig Config;
        [Inject] public readonly PatrolPoints PatrolPoints;
        [Inject] public readonly EnemyAttackAbility AttackAbility;

    }
}
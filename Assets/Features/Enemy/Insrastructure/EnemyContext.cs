using Feature.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Feature.PlayerFeature;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyContext
    {
        [Inject] public readonly Enemy Enemy;
        [Inject] public readonly Player Player;
        [Inject] public readonly EnemyRig EnemyRig;
        [Inject] public readonly IAnimatableEnemy AnimatableEnemy;
        [Inject] public readonly IMovableEnemy MovableEnemy;
        [Inject] public readonly EnemyVisionAndLook EnemyVisionAndLook;
        [Inject] public readonly EnemyConfig Config;
        [Inject] public readonly PatrolPoints PatrolPoints;
        [Inject] public readonly EnemyAttackAbility AttackAbility;

    }
}
using Features.Core.Infrastructure;
using Features.Core.InterfaceAdapters;
using UnityEngine;
using Zenject;

namespace Features.Enemy.Infrastructure.Rig
{
    public class EnemyBinder : MonoBehaviour
    {
        private Entities.Enemy _enemy;
        private IWorldEnemyHeadRegistry _headRegistry;

        [Inject]
        private void Construct(Entities.Enemy enemy, EnemyRefs enemyRefs, IWorldEnemyHeadRegistry headRegistry)
        {
            _enemy = enemy;
            _headRegistry = headRegistry;

            GetComponent<EntityWorldBind>().Bind(enemy, enemy);
            _headRegistry.Bind(enemy, enemyRefs.Head);
        }

        private void OnDestroy()
        {
            _headRegistry?.Unbind(_enemy);
        }
    }
}

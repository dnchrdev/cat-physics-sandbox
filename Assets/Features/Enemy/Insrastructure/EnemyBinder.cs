using Features.Core.Infrastructure.Binder;
using Features.Core.Infrastructure.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Enemy.Insrastructure
{
    public class EnemyBinder : MonoBehaviour
    {
        private EntityWorldBind _entityWorldBind;
        private Domain.Enemy _enemy;
        private IWorldEntityService _worldEntityService;

        [Inject]
        private void Construct(Domain.Enemy enemy, EnemyRig enemyRig, IWorldEntityService worldEntityService)
        {
            _entityWorldBind = GetComponent<EntityWorldBind>();
            _enemy = enemy;
            _entityWorldBind.Bind(enemy, enemy);
            _worldEntityService = worldEntityService;
            _worldEntityService.Bind(enemy, enemy, enemyRig.Head.gameObject);
        }

        public void OnDisable()
        {
            _worldEntityService.Unbind(_enemy);
        }
    }
}
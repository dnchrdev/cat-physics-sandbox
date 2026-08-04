using Feature.Core;
using System;
using UnityEngine;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyBinder : MonoBehaviour
    {
        private EntityWorldBind _entityWorldBind;
        private Enemy _enemy;
        private IWorldEntityService _worldEntityService;

        [Inject]
        private void Construct(Enemy enemy, EnemyRig enemyRig, IWorldEntityService worldEntityService)
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
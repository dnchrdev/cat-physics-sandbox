using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Core.Infrastructure.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Enemy.Insrastructure
{
    [Serializable]
    public struct SpawnAndPatrolPoints
    {
        public Transform SpawnPoint;
        public PatrolPoints PatrolPoints;
    }
    
    public class EnemySpawner : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private SpawnAndPatrolPoints[] _spawnAndPatrolPoints;

        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private IWorldEntityService _worldEntityService;

        private readonly List<Domain.Enemy> _spawnedEnemies = new();

        public void Initialize()
        {
            SpawnAllAsync().Forget();
        }
        
        private async UniTaskVoid SpawnAllAsync()
        {
            if (_spawnAndPatrolPoints == null || _spawnAndPatrolPoints.Length == 0)
            {
                Debug.LogWarning($"{nameof(EnemySpawner)}: no spawn points assigned.", this);
                return;
            }

            var spawnTasks = new List<UniTask<Domain.Enemy>>(_spawnAndPatrolPoints.Length);

            foreach (var point in _spawnAndPatrolPoints)
            {
                var rotation = point.SpawnPoint.rotation;
                spawnTasks.Add(_enemyFactory.CreateAsync(point.SpawnPoint.position, rotation, transform, point.PatrolPoints));
            }

            var result = await UniTask.WhenAll(spawnTasks);

            _spawnedEnemies.AddRange(result);
        }
        
        public void Dispose()
        {
            foreach (var enemy in _spawnedEnemies)
            {
                if (enemy == null)
                    continue;

                var obj = _worldEntityService.GetObjectByEntity(enemy);

                _worldEntityService.Unbind(enemy);

                if (obj != null)
                {
                    Destroy(obj);
                }
            }

            _spawnedEnemies.Clear();

            _enemyFactory.Release();
        }
    }
}
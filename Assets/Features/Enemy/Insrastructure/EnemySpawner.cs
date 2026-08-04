using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Feature.Core;
using Random = UnityEngine.Random;

namespace Feature.EnemyFeature
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

        private readonly List<Enemy> _spawnedEnemies = new();

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

            var spawnTasks = new List<UniTask<Enemy>>(_spawnAndPatrolPoints.Length);

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
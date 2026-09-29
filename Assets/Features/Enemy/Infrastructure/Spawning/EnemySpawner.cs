using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Enemy.Infrastructure.Rig;
using UnityEngine;
using Zenject;

namespace Features.Enemy.Infrastructure.Spawning
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

        private readonly List<GameObject> _spawnedEnemies = new();
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void Initialize()
        {
            SpawnAllAsync().Forget();
        }

        public void Dispose()
        {
            foreach (var enemy in _spawnedEnemies)
                if (enemy != null)
                    Destroy(enemy);

            _spawnedEnemies.Clear();
            _enemyFactory.Release();
        }

        private async UniTaskVoid SpawnAllAsync()
        {
            if (_spawnAndPatrolPoints == null || _spawnAndPatrolPoints.Length == 0)
            {
                Debug.LogWarning($"{nameof(EnemySpawner)}: no spawn points assigned.", this);
                return;
            }

            var spawnTasks = new List<UniTask<GameObject>>(_spawnAndPatrolPoints.Length);
            foreach (var point in _spawnAndPatrolPoints)
            {
                spawnTasks.Add(_enemyFactory.CreateAsync(
                    point.SpawnPoint.position,
                    point.SpawnPoint.rotation,
                    transform,
                    point.PatrolPoints));
            }

            var instances = await UniTask.WhenAll(spawnTasks);
            foreach (var instance in instances)
                if (instance != null)
                    _spawnedEnemies.Add(instance);
        }
    }
}

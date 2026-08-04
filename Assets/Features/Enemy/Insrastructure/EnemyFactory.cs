using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemyFactory
    {
        private readonly DiContainer _container;
        private readonly AssetReferenceGameObject _enemyPrefabRef;

        private AsyncOperationHandle<GameObject> _loadHandle;
        private GameObject _cachedPrefab;
        private bool _isLoading;

        public EnemyFactory(
            DiContainer container,
            [Inject(Id = "EnemyPrefabRef")] AssetReferenceGameObject enemyPrefabRef)
        {
            _container = container;
            _enemyPrefabRef = enemyPrefabRef;
        }

        public async UniTask<Enemy> CreateAsync(
            Vector3 position, Quaternion rotation, Transform parent, PatrolPoints patrolPoints)
        {
            await EnsurePrefabLoadedAsync();

            var subContainer = _container.CreateSubContainer();
            subContainer.Bind<PatrolPoints>().FromInstance(patrolPoints).AsSingle();

            var instance = subContainer.InstantiatePrefab(_cachedPrefab, position, rotation, parent);

            var context = instance.GetComponent<GameObjectContext>();
            if (context == null)
            {
                Debug.LogError($"{nameof(EnemyFactory)}: prefab has no GameObjectContext.", instance);
                Object.Destroy(instance);
                return null;
            }

            return context.Container.Resolve<Enemy>();
        }

        public async UniTask WarmupAsync()
        {
            await EnsurePrefabLoadedAsync();
        }

        private async UniTask EnsurePrefabLoadedAsync()
        {
            if (_cachedPrefab != null)
                return;

            if (_isLoading)
            {
                await UniTask.WaitUntil(() => _cachedPrefab != null);
                return;
            }

            _isLoading = true;

            _loadHandle = Addressables.LoadAssetAsync<GameObject>(_enemyPrefabRef);
            _cachedPrefab = await _loadHandle.ToUniTask();

            _isLoading = false;
        }

        public void Release()
        {
            if (_loadHandle.IsValid())
            {
                Addressables.Release(_loadHandle);
            }

            _cachedPrefab = null;
        }
    }
    
}
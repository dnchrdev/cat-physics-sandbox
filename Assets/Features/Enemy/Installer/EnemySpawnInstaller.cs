using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Feature.EnemyFeature
{
    public class EnemySpawnInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject _enemyPrefabRef;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<AssetReferenceGameObject>()
                .WithId("EnemyPrefabRef")
                .FromInstance(_enemyPrefabRef)
                .AsCached();
            
            Container.Bind<EnemyFactory>().AsSingle();
        }
    }
}

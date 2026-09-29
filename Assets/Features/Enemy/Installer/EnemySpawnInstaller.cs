using Features.Enemy.Infrastructure.Spawning;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Features.Enemy.Installer
{
    public class EnemySpawnInstaller : MonoInstaller
    {
        [SerializeField] private AssetReferenceGameObject _enemyPrefabRef;

        public override void InstallBindings()
        {
            Container.Bind<AssetReferenceGameObject>()
                .WithId("EnemyPrefabRef")
                .FromInstance(_enemyPrefabRef)
                .AsCached();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}

using System;
using Zenject;
using UnityEngine;

namespace Feature.Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private DestroyService destroyService;

        public override void InstallBindings()
        {
            if (destroyService == null) throw new Exception("MISSING REF");

            Container.Bind<IWorldEntityService>().To<WorldEntityService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseService>().AsSingle();
            Container.Bind<ILogger>().To<UnityLogger>().AsSingle();
            Container.Bind<DestroyService>().FromInstance(destroyService).AsSingle();
        }
    }
}
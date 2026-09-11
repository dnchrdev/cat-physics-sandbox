using System;
using Features.Core.Infrastructure.Logger;
using Features.Core.Infrastructure.Services;
using Features.Core.Infrastructure.Services.Interfaces;
using UnityEngine;
using Zenject;
using ILogger = Features.Core.Infrastructure.Interfaces.ILogger;

namespace Features.Core.Installer
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private DestroyService _destroyService;

        public override void InstallBindings()
        {
            if (_destroyService == null) throw new Exception("MISSING REF");

            Container.Bind<IWorldEntityService>().To<WorldEntityService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseService>().AsSingle();
            Container.Bind<ILogger>().To<UnityLogger>().AsSingle();
            Container.Bind<DestroyService>().FromInstance(_destroyService).AsSingle();
        }
    }
}
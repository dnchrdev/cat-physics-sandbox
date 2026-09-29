using Features.Camera.Entities;
using Features.Camera.Infrastructure;
using Features.Camera.Infrastructure.Config;
using Features.Camera.InterfaceAdapters;
using Features.Camera.UseCases;
using Features.Shared.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.Camera.Installer
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraRefs _cameraRefs;
        [SerializeField] private CameraPhysical _cameraPhysical;
        [SerializeField] private CameraConfig _cameraConfig;

        public override void InstallBindings()
        {
            InstallEntities();
            InstallUseCases();
            InstallInterfaceAdapters();
            InstallInfrastructure();
        }

        private void InstallEntities()
        {
            var settings = _cameraConfig.ToSettings();
            Container.Bind<CameraSettings>().FromInstance(settings).AsSingle();
            Container.Bind<CameraRotation>().FromInstance(new CameraRotation(settings.Rotation)).AsSingle();
            Container.Bind<CameraHeadbob>().FromInstance(new CameraHeadbob(settings.Headbob)).AsSingle();
            Container.Bind<CameraLean>().FromInstance(new CameraLean(settings.Lean)).AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<CameraFollowUseCase>().AsSingle();
            Container.Bind<CameraRotationUseCase>().AsSingle();
            Container.Bind<CameraLeanUseCase>().AsSingle();
            Container.Bind<CameraHeadbobUseCase>().AsSingle();
            Container.Bind<FollowEnemyHeadUseCase>().AsSingle();
            Container.Bind<ResetCameraUseCase>().AsSingle();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<CameraController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraMover>().AsSingle();
            Container.BindInterfacesTo<CameraAdapter>().AsSingle();
            Container.BindInterfacesTo<EnemyHeadPositionAdapter>().AsSingle();
        }

        private void InstallInfrastructure()
        {
            Container.Bind<CameraRefs>().FromInstance(_cameraRefs).AsSingle();
            Container.Bind<IReadOnlyCameraPhysical>().FromInstance(_cameraPhysical).AsSingle();
        }
    }
}

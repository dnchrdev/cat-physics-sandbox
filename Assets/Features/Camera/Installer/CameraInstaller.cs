using Feature.CameraFeature;
using UnityEngine;
using Zenject;

namespace Assets.Features.CustomCamera
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraRig _cameraRig;
        [SerializeField] private CameraPhysics _cameraPhysics;
        [SerializeField] private CameraConfig _cameraConfig;

        public override void InstallBindings()
        {
            Container.Bind<CameraRig>().FromInstance(_cameraRig).AsSingle();

            Container.Bind<CameraConfig>().FromInstance(_cameraConfig).AsSingle();

            Container.Bind<CameraHeadbob>().AsSingle();
            Container.Bind<CameraPosition>().AsSingle();
            Container.Bind<CameraRotation>().AsSingle();
            Container.Bind<CameraLean>().AsSingle();

            Container.Bind<PlayerDeadCameraRotation>().AsSingle();

            Container.Bind<PlayerRespawnedUseCase>().AsSingle();

            Container.BindInterfacesTo<CameraOrchestrator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraPhysics>().FromInstance(_cameraPhysics).AsSingle();
        }
    }
}
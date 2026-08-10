using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerRig _playerRig;
        [SerializeField] private CharacterMotor _physicsController;
        [SerializeField] private PlayerCharacterConfig playerCharacterConfig;
        [SerializeField] private SurfacesConfig _surfaces;
        [SerializeField] private MobileControlView  _mobileControlView;
        [SerializeField] private PCGameplayView _pcGameplayView;
        [SerializeField] private KnockoutView _knockoutView;

        public override void InstallBindings()
        {
            //Domain
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementState>().AsSingle();

            //Application
            Container.Bind<SlidePhysicsCalculator>().AsSingle();
            Container.BindInterfacesTo<CharacterStateMachine>().AsSingle();
            Container.Bind<PlayerRespawnUseCase>().AsSingle();
            Container.Bind<PlayerGameStartedUseCase>().AsSingle();
            
            //Adapter

            //Infrastructure
            Container.Bind<MovementContext>().AsSingle();
            Container.Bind<PlayerRig>().FromInstance(_playerRig).AsSingle();

            Container.BindInterfacesTo<CharacterMotor>().FromInstance(_physicsController).AsSingle();

            Container.Bind<PlayerCharacterConfig>().FromInstance(playerCharacterConfig).AsSingle();
            Container.Bind<SurfacesConfig>().FromInstance(_surfaces).AsSingle();
            Container.Bind<SurfaceDetector>().AsSingle();

            //Presentation
            Container.BindInterfacesAndSelfTo<GameplayControlPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<KnockoutPresenter>().AsSingle();
            
            Container.BindInterfacesTo<MobileControlView>().FromInstance(_mobileControlView).AsSingle();
            Container.BindInterfacesTo<PCGameplayView>().FromInstance(_pcGameplayView).AsSingle();
            Container.BindInterfacesTo<KnockoutView>().FromInstance(_knockoutView).AsSingle();
        }
    }
}
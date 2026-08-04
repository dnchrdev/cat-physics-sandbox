using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerRig _playerRig;
        [SerializeField] private CharacterMotor _physicsController;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private SurfacesConfigSO _surfacesConfig;
        [SerializeField] private PCGameplayPresenter _pcGameplayPanel;
        [SerializeField] private MobileControlPresenter _mobileGameplayPanel;
        [SerializeField] private PlayerKnockoutPanel _knockoutView;

        public override void InstallBindings()
        {
            //Domain
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementState>().AsSingle();

            //Application
            Container.Bind<SlidePhysicsCalculator>().AsSingle();
            Container.Bind<DeadCharacterState>().AsSingle();
            Container.Bind<WalkCharacterState>().AsSingle();
            Container.Bind<JumpCharacterState>().AsSingle();
            Container.Bind<AirborneCharacterState>().AsSingle();

            Container.BindInterfacesTo<CharacterStateMachine>().AsSingle();
            Container.Bind<PlayerRespawnUseCase>().AsSingle();
            Container.Bind<PlayerGameStartedUseCase>().AsSingle();

            //Infrastructure
            Container.Bind<MovementContext>().AsSingle();
            Container.Bind<PlayerRig>().FromInstance(_playerRig).AsSingle();

            Container.BindInterfacesTo<CharacterMotor>().FromInstance(_physicsController).AsSingle();

            Container.Bind<CharacterConfig>().FromInstance(_characterConfig).AsSingle();
            Container.Bind<SurfacesConfigSO>().FromInstance(_surfacesConfig).AsSingle();
            Container.Bind<SurfaceDetector>().AsSingle();

            //Presentation
            Container.BindInterfacesAndSelfTo<PlayerPresenterOrchestrator>().AsSingle();

            Container.Bind<MobileControlPresenter>().FromInstance(_mobileGameplayPanel).AsSingle();
            Container.Bind<PCGameplayPresenter>().FromInstance(_pcGameplayPanel).AsSingle();
            Container.Bind<PlayerKnockoutPanel>().FromInstance(_knockoutView).AsSingle();
        }
    }
}
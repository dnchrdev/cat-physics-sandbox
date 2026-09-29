using Features.Player.Entities;
using Features.Player.Infrastructure;
using Features.Player.Infrastructure.Configs;
using Features.Player.Infrastructure.Views;
using Features.Player.InterfaceAdapters.Controllers;
using Features.Player.InterfaceAdapters.Gateways;
using Features.Player.InterfaceAdapters.Presenters;
using Features.Player.UseCases;
using Features.Player.UseCases.Movement;
using Features.Player.UseCases.Ports;
using UnityEngine;
using Zenject;

namespace Features.Player.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerRefs playerRefs;
        [SerializeField] private CharacterMotor _characterMotor;
        [SerializeField] private PlayerCharacterConfig _playerCharacterConfig;
        [SerializeField] private SurfacesConfig _surfacesConfig;
        [SerializeField] private MobileControlView _mobileControlView;
        [SerializeField] private PCGameplayView _pcGameplayView;
        [SerializeField] private KnockoutView _knockoutView;

        public override void InstallBindings()
        {
            InstallEntities();
            InstallUseCases();
            InstallInterfaceAdapters();
            InstallInfrastructure();
        }

        private void InstallEntities()
        {
            Container.BindInterfacesAndSelfTo<Entities.Player>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementState>().AsSingle();
            Container.Bind<MovementRules>().FromInstance(_playerCharacterConfig.ToRules()).AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<WalkMode>().AsSingle();
            Container.Bind<AirborneMode>().AsSingle();
            Container.Bind<JumpMode>().AsSingle();
            Container.Bind<CharacterMovementUseCase>().AsSingle();

            Container.Bind<PlayerRespawnUseCase>().AsSingle();
            Container.BindInterfacesTo<GameStartedUseCase>().AsSingle();
            Container.Bind<SyncPlayerPositionUseCase>().AsSingle();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<PlayerMovementController>().AsSingle();
            Container.BindInterfacesTo<GameplayControlsController>().AsSingle();
            Container.BindInterfacesTo<KnockoutPresenter>().AsSingle();
            Container.Bind<ISurfaceProvider>().To<SurfaceGateway>().AsSingle();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesTo<PlayerRefs>().FromInstance(playerRefs).AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterMotor>().FromInstance(_characterMotor).AsSingle();
            Container.Bind<SurfacesConfig>().FromInstance(_surfacesConfig).AsSingle();
            Container.BindInterfacesTo<MobileControlView>().FromInstance(_mobileControlView).AsSingle();
            Container.BindInterfacesTo<PCGameplayView>().FromInstance(_pcGameplayView).AsSingle();
            Container.BindInterfacesTo<KnockoutView>().FromInstance(_knockoutView).AsSingle();
        }
    }
}

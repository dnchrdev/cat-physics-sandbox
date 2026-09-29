using Features.Interaction.Infrastructure;
using Features.Interaction.Infrastructure.Configs;
using Features.Interaction.Infrastructure.Interactable;
using Features.Interaction.Infrastructure.Rig;
using Features.Interaction.Infrastructure.View;
using Features.Interaction.InterfaceAdapters.Controllers;
using Features.Interaction.InterfaceAdapters.Gateways;
using Features.Interaction.InterfaceAdapters.Ports;
using Features.Interaction.InterfaceAdapters.Presenters;
using Features.Interaction.UseCases;
using Features.Interaction.UseCases.Ports;
using Features.Shared.Entities.Ports;
using UnityEngine;
using Zenject;

namespace Features.Interaction.Installer
{
    public sealed class InteractionInstaller : MonoInstaller
    {
        [SerializeField] private InteractableHandRefs _handRefs;
        [SerializeField] private InteractionRootBehaviour _interactionRoot;
        [SerializeField] private InteractionControllerConfig _interactionControllerConfig;
        [SerializeField] private InteractionHintsShower _pcInteractionHintsShower;
        [SerializeField] private InteractionHintsShower _mobileInteractionHintsShower;
        [SerializeField] private InteractionIndicator _interactionIndicator;

        private IReadOnlyControlSettings _controlSettings;

        [Inject]
        private void Construct(IReadOnlyControlSettings controlSettings)
        {
            _controlSettings = controlSettings;
        }

        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesAndSelfTo<InteractionControllerConfig>().FromInstance(_interactionControllerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<InteractableHandRefs>().FromInstance(_handRefs).AsSingle();
            Container.BindInterfacesTo<InteractionRootBehaviour>().FromInstance(_interactionRoot).AsSingle();

            var hintsShower = _controlSettings.IsMobile ? _mobileInteractionHintsShower : _pcInteractionHintsShower;
            Container.BindInterfacesTo<InteractionHintsShower>().FromInstance(hintsShower).AsSingle();
            Container.BindInterfacesTo<InteractionIndicator>().FromInstance(_interactionIndicator).AsSingle();
            Container.Bind<IScreenProjection>().To<ScreenProjection>().AsSingle();

            Container.Bind<EntityBindResolver>().AsSingle();
            Container.Bind<InteractableFactory>().AsSingle();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<InteractionController>().AsSingle();
            Container.BindInterfacesTo<InteractionRootController>().AsSingle();
            Container.BindInterfacesTo<InteractionPresenter>().AsSingle();
            Container.Bind<IInteractionDetection>().To<InteractionDetectionGateway>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<FocusUseCase>().AsSingle();
            Container.Bind<GrabUseCase>().AsSingle();
            Container.Bind<InteractionUseCase>().AsSingle();
            Container.Bind<InteractionRootFollowUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<InteractableResetUseCase>().AsSingle();
        }
    }
}

using Feature.Storage;
using Feature.UI;
using System;
using UnityEngine;
using Zenject;

namespace Feature.PhysicsInteraction
{
    public class PhysicsInteractionInstaller : MonoInstaller
    {
        [SerializeField] private InteractableHandRig _handRig;
        [SerializeField] private InteractionRoot _interactionRoot;
        [SerializeField] private InteractionControllerConfig _interactionControllerConfig;
        [SerializeField] private InteractionTipShower _pcInteractionTipShower;
        [SerializeField] private InteractionTipShower _mobileInteractionTipShower;
        [SerializeField] private InteractionIndication _interactionIndication;

        private IReadOnlyControlSettings _controlSettings;

        [Inject]
        private void Construct(IReadOnlyControlSettings controlSettings)
        {
            _controlSettings = controlSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InteractableResetService>().AsSingle();
            Container.Bind<InteractionComponentsFactory>().AsSingle();
            Container.Bind<EntityBindResolver>().AsSingle();    

            if (_controlSettings.IsMobile)
            {
                Container.Bind<InteractionTipShower>().FromInstance(_mobileInteractionTipShower).AsSingle();
            }
            else
            {
                Container.Bind<InteractionTipShower>().FromInstance(_pcInteractionTipShower).AsSingle();
            }

            Container.Bind<InteractionIndication>().FromInstance(_interactionIndication).AsSingle();
            Container.Bind<InteractableHandRig>().FromInstance(_handRig).AsSingle();
            Container.Bind<InteractionControllerConfig>().FromInstance(_interactionControllerConfig).AsSingle();
            Container.Bind<InteractionRoot>().FromInstance(_interactionRoot).AsSingle();
            Container.BindInterfacesTo<InteractionRootController>().AsSingle();
            Container.BindInterfacesTo<InteractionInputController>().AsSingle().NonLazy();
  
        }
    }
}
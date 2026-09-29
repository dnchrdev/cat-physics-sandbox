using Features.Interaction.Entities;
using Features.Interaction.Infrastructure.Configs;
using Features.Interaction.Infrastructure.Rig;
using Features.Interaction.Infrastructure.View;
using Features.Interaction.InterfaceAdapters.Gateways;
using Features.Interaction.InterfaceAdapters.Presenters;
using Features.Interaction.UseCases;
using UnityEngine;

namespace Features.Interaction.Infrastructure.Interactable
{
    public sealed class InteractableFactory
    {
        private readonly InteractableHandRefs _hand;
        private readonly InteractableResetUseCase _reset;

        public InteractableFactory(InteractableHandRefs hand, InteractableResetUseCase reset)
        {
            _hand = hand;
            _reset = reset;
        }

        public InteractableEntity Create(InteractableConfig config, Rigidbody rb, MeshRenderer meshRenderer, Collider collider)
        {
            var body = new InteractableBodyGateway(rb, collider, config.InteractablePhysicsConfig);
            var joint = new GrabJointGateway(rb, _hand.HandRigidbody);
            var entity = new InteractableEntity(config.ToSettings(), body, joint);

            var highlightView = new InteractableHighlightView(meshRenderer, config.InteractableVisualConfig);
            new InteractableHighlightPresenter(entity, highlightView);

            _reset.Register(entity);
            return entity;
        }
    }
}

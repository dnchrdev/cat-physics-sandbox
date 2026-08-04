using Feature.Core;
using System;
using System.Collections.Generic;

namespace Feature.PhysicsInteraction
{
    public class InteractionComponentsFactory
    {
        private readonly DestroyService _destroyService;
        private readonly EntityBindResolver _entityBindResolver;    
        
        private readonly Dictionary<GrabType, Func<IGrabable>> _grabables;
        private readonly Dictionary<HitType, Func<IHitable>> _hitables;
        private readonly Dictionary<CollisionHitType, Func<IColliderHit>> _hitHandlers;
        private readonly Dictionary<CollisionTriggerType, Func<IColliderTrigger>> _triggerHandlers;

        public InteractionComponentsFactory(DestroyService destroyService, EntityBindResolver entityBindResolver)
        {
            _destroyService = destroyService;
            _entityBindResolver = entityBindResolver;
            
            _grabables = new Dictionary<GrabType, Func<IGrabable>>
        {
            { GrabType.CenterWithRotation,
                () => new GrabCenterWithRotationStrategy(_destroyService) },
            { GrabType.Center,
                () => new GrabCenterStrategy(_destroyService) },
            { GrabType.NearestPoint,
                () => new GrabNearestPointStrategy(_destroyService) },
        };

            _hitables = new Dictionary<HitType, Func<IHitable>>
        {
            { HitType.Impulse, () => new HitStrategy() },
        };

            _hitHandlers = new Dictionary<CollisionHitType, Func<IColliderHit>>
        {
            { CollisionHitType.CollisionHit, () => new ColliderHitStrategy(_entityBindResolver) },
        };

            _triggerHandlers = new Dictionary<CollisionTriggerType, Func<IColliderTrigger>>
        {
            { CollisionTriggerType.CollisionTrigger, () => new CoolliderTriggerStrategy(_entityBindResolver) },
        };
        }

        public IGrabable Create(GrabType type) => Resolve(_grabables, type);
        public IHitable Create(HitType type) => Resolve(_hitables, type);
        public IColliderHit Create(CollisionHitType type) => Resolve(_hitHandlers, type);
        public IColliderTrigger Create(CollisionTriggerType type) => Resolve(_triggerHandlers, type);

        private TResult Resolve<TKey, TResult>( Dictionary<TKey, Func<TResult>> map, TKey key) where TResult : class
            => map.TryGetValue(key, out var factory) ? factory() : null;
    }
}
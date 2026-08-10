using System;
using Feature.CameraFeature;
using Feature.PhysicsInteractio;
using Feature.Shared;
using UnityEngine;
using Zenject;

namespace Feature.PhysicsInteraction
{
    public class Interactable : MonoBehaviour, IResetable, IQuestInteractable
    {
        public event Action Grabbed;
        public event Action Released;
        public event Action Throwed;
        public event Action Hitted;
        public event Action ColliderHit;
        public event Action ColliderTrigger;

        public event Action<IQuestInteractable> QuestHitEvent;
        public event Action<IQuestInteractable> QuestThrowEvent;
        public event Action<IQuestInteractable, Collision> QuestColliderHitEvent;

        [SerializeField] private InteractableConfig _config;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Collider _collider;
        [SerializeField] private HitEnter _colliderHitEvent;
        [SerializeField] private TriggerEnter _colliderTriggerEvent;

        private InteractablePhysics _physics;
        private InteractableVisual _visual;
        private InteractableOwnership _ownership;
        private InteractableState _state;

        private InteractableResetService _interactableResetService;
        private InteractionComponentsFactory _interactionComponentsFactory;

        private IHitable _hitable;
        private IGrabable _grabable;
        private IColliderHit _hitHandler;
        private IColliderTrigger _triggerHandler;

        private InteractionType _interactionType;

        public bool IsGrabbed => _grabable?.IsGrabbed() ?? false;
        public bool UseGrabPositionIndication => _config.InteractableVisualConfig.UseGrabPositionIndication;
        public InteractionType InteractionType => _interactionType;
        public Vector3 Anchor => _config.AnchorDirection;
        public bool UseCustomHandDistance => _config.UseCustomHandDistance;
        public float CustomHandDistance => _config.CustomHandDistance;
        public Transform GetTransform() => transform;
        public Rigidbody GetRigidbody() => _rb;

        [Inject]
        private void Construct(
            InteractionComponentsFactory interactionComponentsFactory,
            InteractableResetService interactableResetService)
        {
            _interactionComponentsFactory = interactionComponentsFactory;
            _interactableResetService = interactableResetService;
        }

        private void Awake()
        {
            if (_config == null || _config.IsRagdoll) return;

            Initialize(_config, _rb, _meshRenderer, _collider, _colliderHitEvent);
            _interactableResetService.AddItem(this);
        }

        public void Initialize(
            InteractableConfig config,
            Rigidbody rb,
            MeshRenderer meshRenderer,
            Collider collider,
            HitEnter hitEnter)
        {
            _config = config;
            _rb = rb;
            _meshRenderer = meshRenderer;
            _collider = collider;
            _colliderHitEvent = hitEnter;

            _physics = new InteractablePhysics(_rb, _collider, _config.InteractablePhysicsConfig, _config.IsKinematicOnStart,
                _config.IsRagdoll);
            _visual = new InteractableVisual(_meshRenderer, _config.InteractableVisualConfig);
            _ownership = new InteractableOwnership();
            _state = new InteractableState(transform, _physics, _config.IsKinematicOnStart);

            _hitHandler = _interactionComponentsFactory.Create(_config.CollisionHitType);
            _triggerHandler = _interactionComponentsFactory.Create(_config.CollisionTriggerType);
            _hitable = _interactionComponentsFactory.Create(_config.HitType);
            _grabable = _interactionComponentsFactory.Create(_config.GrabType);

            ResolveInteractionType();
            SubscribeEvents();

            _visual.Reset();
            _physics.SetDefaultPhysics();
        }

        public bool TryGrab(Rigidbody hand, IEntity owner, Vector3 impactPosition)
        {
            if (_grabable == null) return false;
            if (!_grabable.Grab(hand, _rb, _collider, impactPosition)) return false;

            _ownership.SetOwner(owner);
            Grabbed?.Invoke();
            
            _physics.SetGrabPhysics();
            return true;
        }

        public void Throw(Vector3 toPoint, float power)
        {
            Throwed?.Invoke();
            QuestThrowEvent?.Invoke(this);

            _visual.Reset();
            _physics.SetDefaultPhysics();
            _grabable?.Throw(toPoint, power, _rb);
        }

        public void Release()
        {
            Released?.Invoke();

            _visual.Reset();
            _physics.SetDefaultPhysics();
            _grabable?.Release();
        }

        public void Hit(Vector3 direction, Vector3 atPoint, float power, IEntity owner)
        {
            Hitted?.Invoke();
            QuestHitEvent?.Invoke(this);

            _ownership.SetOwner(owner);
            _hitable?.Hit(direction, atPoint, power, _rb);
        }

        public void ShowFocusVisual() => _visual.ShowVisualOnFocus();
        public void ShowGrabVisual() => _visual.ShowVisualOnGrab();
        public void ResetVisual() => _visual.Reset();

        public Vector3 GetGrabPosition(Vector3 handPosition, IReadOnlyCamera camera, LayerMask interactionMask) => _grabable?.GetGrabPosition(handPosition, camera, interactionMask) ?? handPosition;

        public void UpdateInteractableAnchor(Vector3 grabPosition)
        {
            if(_grabable == null)  return;
            
            _grabable.UpdateAnchor(grabPosition);
        }
        
        public void SaveState() => _state.SaveState();
        public void ResetState() => _state.ResetState();

        private void SubscribeEvents()
        {
            if (_colliderHitEvent != null)
                _colliderHitEvent.OnHitEvent += OnColliderHitEvent;

            if (_colliderTriggerEvent != null)
                _colliderTriggerEvent.OnTriggerEnterEvent += OnColliderTriggerEvent;
        }

        private void OnColliderHitEvent(Collision collision)
        {
            ColliderHit?.Invoke();

            if (IsGrabbed) return;

            var owner = _ownership.GetOwner();
            _ownership.ClearOwner();
            
            QuestColliderHitEvent?.Invoke(this, collision);

            if (_hitHandler == null) return;
            if (owner == null) return;
            
            _hitHandler.HandleHit(owner, _rb, collision);
        }

        private void OnColliderTriggerEvent(Collider collider)
        {
            ColliderTrigger?.Invoke();

            if (IsGrabbed) return;

            var owner = _ownership.GetOwner();
            _ownership.ClearOwner();

            if (_triggerHandler == null) return;

            if (owner == null) return;

            _triggerHandler.HandleTrigger(owner, collider);
        }

        private void ResolveInteractionType()
        {
            _interactionType = (_hitable, _grabable) switch
            {
                (null, null) => InteractionType.None,
                (not null, not null) => InteractionType.Full,
                (not null, null) => InteractionType.Hit,
                (null, not null) => InteractionType.Grab,
            };
        }
    }
}
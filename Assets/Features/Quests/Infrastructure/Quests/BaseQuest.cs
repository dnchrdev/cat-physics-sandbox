using System;
using System.Collections.Generic;
using Features.Interaction.Infrastructure;
using Features.Interaction.Infrastructure.Interactable;
using Features.Quests.Entities;
using Features.Quests.Entities.Objectives;
using Features.Quests.Infrastructure.Targets;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using UnityEngine;
using Zenject;

namespace Features.Quests.Infrastructure.Quests
{
    public class BaseQuest : MonoBehaviour, IQuestSource
    {
        [field: SerializeField] public string QuestName { get; private set; }
        [SerializeField] protected List<InteractableBehaviour> Targets;
        [SerializeField] private List<Transform> _additionalTipTransforms;

        public event Action<string, QuestTargetEvent> TargetEventOccurred;

        private readonly List<Action> _unsubscribers = new();
        private EntityBindResolver _bindResolver;
        private List<IHintAnchor> _tipAnchors;

        public int TargetCount => Targets.Count;

        public IReadOnlyList<IHintAnchor> TipAnchors =>
            _tipAnchors ??= _additionalTipTransforms.ConvertAll<IHintAnchor>(tip => new TransformHintAnchor(tip));

        [Inject]
        private void Construct(EntityBindResolver bindResolver)
        {
            _bindResolver = bindResolver;
        }

        public virtual QuestObjective CreateObjective() => new NoProgressObjective();

        public IHintAnchor GetTargetAnchor(int targetIndex) => new TransformHintAnchor(Targets[targetIndex].transform);

        public void StartReporting()
        {
            StopReporting();

            for (var i = 0; i < Targets.Count; i++)
                Subscribe(i, Targets[i]);
        }

        public void StopReporting()
        {
            foreach (var unsubscribe in _unsubscribers)
                unsubscribe();

            _unsubscribers.Clear();
        }

        private void OnDestroy() => StopReporting();

        private void Subscribe(int index, InteractableBehaviour target)
        {
            Action onHit = () => Invoke(QuestTargetEvent.Hit(index));
            Action<Collision> onCollided = collision => Invoke(ToCollisionEvent(index, target, collision));

            target.Hit += onHit;
            target.Thrown += onHit;
            target.Collided += onCollided;

            _unsubscribers.Add(() =>
            {
                target.Hit -= onHit;
                target.Thrown -= onHit;
                target.Collided -= onCollided;
            });
        }

        private QuestTargetEvent ToCollisionEvent(int index, InteractableBehaviour target, Collision collision)
        {
            var body = target.GetRigidbody();
            var speed = body != null ? body.linearVelocity.magnitude : 0f;
            var other = _bindResolver.ResolveAsEntity(collision.collider);
            var team = other != null ? other.Team : TeamType.None;

            return QuestTargetEvent.Collided(index, target.transform.position.y, speed, team);
        }

        private void Invoke(QuestTargetEvent targetEvent) => TargetEventOccurred?.Invoke(QuestName, targetEvent);
    }
}

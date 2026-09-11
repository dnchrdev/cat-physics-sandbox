using Features.PhysicsInteraction.Adapter.Interaction;
using Features.Quests.Domain;
using Features.Quests.Infrastructure.Interfaces;
using UnityEngine;

namespace Features.Quests.Infrastructure.Quests
{
    public class DropQuest : BaseQuest
    {
        [SerializeField] private float _minY;

        public override void Init(QuestsCollection questCollection, EntityBindResolver entityBindResolver)
        {
            base.Init(questCollection,  entityBindResolver);

            foreach (var interactable in Targets)
            {
                interactable.QuestColliderHitEvent += OnHit;
            }
        }   

        public override void Dispose()
        {
            foreach (var interactable in Targets)
            {
                interactable.QuestColliderHitEvent -= OnHit;
            }
        }

        void OnHit(IQuestInteractable item, Collision col)
        {
            if (item.GetTransform().position.y < _minY)
            {
                if (TryToDoneTarget(item) == false) return;

                QuestsCollection.AddProgress(Name);
            }
        }
    }
}

using Features.PhysicsInteraction.Adapter.Interaction;
using Features.Quests.Domain;
using Features.Quests.Infrastructure.Interfaces;
using Features.Shared;
using UnityEngine;

namespace Features.Quests.Infrastructure.Quests
{
    public class HitEnemyQuest: BaseQuest
    {
        [SerializeField] private float _minInteractableHitVelocity = 0.1f;
        
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

        void OnHit(IQuestInteractable item, Collision collision)
        {
            if(item.GetRigidbody().linearVelocity.magnitude < _minInteractableHitVelocity) return;
            
            var bind = EntityBindResolver.ResolveEntityBind(collision.collider);
            
            if (bind == null) return;
            
            if(bind.AsEntity.Team == TeamType.Enemy)
                QuestsCollection.AddProgress(Name);
            
        }

        
    }
}
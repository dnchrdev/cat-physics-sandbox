using Feature.Core;
using Feature.EnemyFeature;
using Feature.PhysicsInteraction;
using Feature.Shared;
using UnityEngine;

namespace Feature.Quests
{
    public class HitEnemyQuest2: BaseQuest
    {
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

        void OnHit(IQuestInteractable item, Collision collision, Rigidbody rb)
        {
            var bind = EntityBindResolver.ResolveEntityBind(collision.collider);
            
            if (bind == null) return;
            
            if(bind.AsEntity.Team == TeamType.Enemy)
                QuestsCollection.AddProgress(Name);
            
        }

        
    }
}
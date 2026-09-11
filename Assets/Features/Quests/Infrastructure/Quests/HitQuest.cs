using Features.PhysicsInteraction.Adapter.Interaction;
using Features.Quests.Domain;
using Features.Quests.Infrastructure.Interfaces;

namespace Features.Quests.Infrastructure.Quests
{
    public class HitQuest : BaseQuest
    {
        public override void Init(QuestsCollection questCollection, EntityBindResolver entityBindResolver)
        {
            base.Init(questCollection,  entityBindResolver);

            foreach (var interactable in Targets)
            {
                interactable.QuestHitEvent += OnHit;
                interactable.QuestThrowEvent += OnHit;
            }
        }

        public override void Dispose()
        {
            foreach (var interactable in Targets)
            {
                interactable.QuestHitEvent -= OnHit;
                interactable.QuestThrowEvent -= OnHit;
            }
        }

        void OnHit(IQuestInteractable item)
        {
            if (TryToDoneTarget(item) == false) return;

            QuestsCollection.AddProgress(Name);
        }
    }
}

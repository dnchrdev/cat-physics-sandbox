using Features.Quests.Entities.Objectives;
using UnityEngine;

namespace Features.Quests.Infrastructure.Quests
{
    public class HitEnemyQuest : BaseQuest
    {
        [SerializeField] private float _minHitVelocity = 0.1f;

        public override QuestObjective CreateObjective() => new HitEnemyObjective(_minHitVelocity);
    }
}

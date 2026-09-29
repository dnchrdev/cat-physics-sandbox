using Features.Shared.Entities;

namespace Features.Quests.Entities.Objectives
{
    public sealed class HitEnemyObjective : QuestObjective
    {
        private readonly float _minSpeed;

        public HitEnemyObjective(float minSpeed)
        {
            _minSpeed = minSpeed;
        }

        public override bool TryCount(QuestTargetEvent targetEvent) =>
            targetEvent.Action == QuestTargetAction.Collided &&
            targetEvent.Speed >= _minSpeed &&
            targetEvent.CollidedWith == TeamType.Enemy;
    }
}

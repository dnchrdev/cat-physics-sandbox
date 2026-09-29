namespace Features.Quests.Entities.Objectives
{
    public sealed class NoProgressObjective : QuestObjective
    {
        public override bool TryCount(QuestTargetEvent targetEvent) => false;
    }
}

namespace Features.Quests.Entities.Objectives
{
    public sealed class StrikeTargetsObjective : EachTargetOnceObjective
    {
        protected override bool Matches(QuestTargetEvent targetEvent) =>
            targetEvent.Action == QuestTargetAction.Hit;
    }
}

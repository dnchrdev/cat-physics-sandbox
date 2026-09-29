namespace Features.Quests.Entities.Objectives
{
    public sealed class DropTargetsObjective : EachTargetOnceObjective
    {
        private readonly float _belowHeight;

        public DropTargetsObjective(float belowHeight)
        {
            _belowHeight = belowHeight;
        }

        protected override bool Matches(QuestTargetEvent targetEvent) =>
            targetEvent.Action == QuestTargetAction.Collided && targetEvent.Height < _belowHeight;
    }
}

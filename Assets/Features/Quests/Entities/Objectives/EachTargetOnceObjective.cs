using System.Collections.Generic;

namespace Features.Quests.Entities.Objectives
{
    public abstract class EachTargetOnceObjective : QuestObjective
    {
        private readonly HashSet<int> _doneTargets = new();

        public sealed override bool TryCount(QuestTargetEvent targetEvent) =>
            Matches(targetEvent) && _doneTargets.Add(targetEvent.TargetIndex);

        public sealed override bool IsTargetDone(int targetIndex) => _doneTargets.Contains(targetIndex);

        public sealed override void Reset() => _doneTargets.Clear();

        protected abstract bool Matches(QuestTargetEvent targetEvent);
    }
}

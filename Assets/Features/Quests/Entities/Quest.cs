using System.Collections.Generic;
using Features.Quests.Entities.Objectives;

namespace Features.Quests.Entities
{
    public sealed class Quest
    {
        private readonly QuestObjective _objective;
        private int _progress;
        private bool _hintsVisible;

        public string Name { get; }
        public string Description { get; }
        public int TargetProgress { get; }
        public int TargetCount { get; }

        public Quest(string name, string description, int targetProgress, int targetCount, QuestObjective objective)
        {
            Name = name;
            Description = description;
            TargetProgress = targetProgress;
            TargetCount = targetCount;
            _objective = objective;
        }

        public float ProgressRatio => _progress * 1f / TargetProgress;
        public bool IsCompleted => _progress >= TargetProgress;
        public bool IsHintsVisible => !IsCompleted && _hintsVisible;

        public bool Record(QuestTargetEvent targetEvent)
        {
            if (IsCompleted || !_objective.TryCount(targetEvent))
                return false;

            _progress += 1;
            return true;
        }

        public bool TryShowHints()
        {
            if (IsCompleted || _hintsVisible)
                return false;

            _hintsVisible = true;
            return true;
        }

        public IEnumerable<int> RemainingTargets()
        {
            for (var i = 0; i < TargetCount; i++)
            {
                if (!_objective.IsTargetDone(i))
                    yield return i;
            }
        }

        public void Reset()
        {
            _progress = 0;
            _hintsVisible = false;
            _objective.Reset();
        }
    }
}

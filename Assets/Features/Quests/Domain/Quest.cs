using System.Collections;
using UnityEngine;

namespace Feature.Quests
{
    public class Quest
    {
        public readonly string Name;
        public readonly string Description;
        public readonly int TargetProgress;
        public readonly BaseQuest BaseQuest;

        private int _currentProgress;
        private bool _visibleTips;

        public Quest(string name, int targetProgress, string description, BaseQuest baseQuest)
        {
            Name = name;
            TargetProgress = targetProgress;
            _currentProgress = 0;
            Description = description;
            BaseQuest = baseQuest;
            _visibleTips = false;
        }

        public float ProgressRatio => _currentProgress * 1f / TargetProgress;
        public bool IsCompleted => _currentProgress >= TargetProgress;
        public bool IsHintsVisible => IsCompleted == false && _visibleTips;

        public bool TryAddProgress()
        {
            if (IsCompleted) return false;

            _currentProgress += 1;
            return true;
        }

        public bool TryShowTips()
        {
            if (IsCompleted || _visibleTips == true) return false;

            _visibleTips = true;
            return true;
        }

        public void ResetProgress()
        {
            _currentProgress = 0;
            _visibleTips = false;
        }
    }
}
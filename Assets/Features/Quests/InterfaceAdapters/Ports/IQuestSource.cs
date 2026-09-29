using System;
using System.Collections.Generic;
using Features.Quests.Entities;

namespace Features.Quests.InterfaceAdapters.Ports
{
    public interface IQuestSource
    {
        event Action<string, QuestTargetEvent> TargetEventOccurred;

        string QuestName { get; }
        IReadOnlyList<IHintAnchor> TipAnchors { get; }

        void StartReporting();
        void StopReporting();
        IHintAnchor GetTargetAnchor(int targetIndex);
    }
}

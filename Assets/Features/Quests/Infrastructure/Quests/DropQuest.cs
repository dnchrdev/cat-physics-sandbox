using Features.Quests.Entities.Objectives;
using UnityEngine;

namespace Features.Quests.Infrastructure.Quests
{
    public class DropQuest : BaseQuest
    {
        [SerializeField] private float _minY;

        public override QuestObjective CreateObjective() => new DropTargetsObjective(_minY);
    }
}

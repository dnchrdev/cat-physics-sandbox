using Features.Quests.Entities.Objectives;

namespace Features.Quests.Infrastructure.Quests
{
    public class HitQuest : BaseQuest
    {
        public override QuestObjective CreateObjective() => new StrikeTargetsObjective();
    }
}

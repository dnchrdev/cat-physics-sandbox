namespace Features.Quests.Entities.Objectives
{
    public abstract class QuestObjective
    {
        public abstract bool TryCount(QuestTargetEvent targetEvent);
        public virtual bool IsTargetDone(int targetIndex) => false;
        public virtual void Reset() { }
    }
}

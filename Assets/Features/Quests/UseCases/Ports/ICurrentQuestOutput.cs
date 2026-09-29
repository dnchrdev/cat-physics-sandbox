namespace Features.Quests.UseCases.Ports
{
    public interface ICurrentQuestOutput
    {
        void ShowCurrentQuest(string description, float progress);
    }
}

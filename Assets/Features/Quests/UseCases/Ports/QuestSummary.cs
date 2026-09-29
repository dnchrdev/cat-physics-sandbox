namespace Features.Quests.UseCases.Ports
{
    public readonly struct QuestSummary
    {
        public string Name { get; }
        public string Description { get; }
        public float Progress { get; }

        public QuestSummary(string name, string description, float progress)
        {
            Name = name;
            Description = description;
            Progress = progress;
        }
    }
}

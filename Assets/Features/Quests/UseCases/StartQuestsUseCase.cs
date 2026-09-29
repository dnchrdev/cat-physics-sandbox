using Features.GameLifeCycle.UseCases.Ports;
using Features.Quests.Entities;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.UseCases
{
    public sealed class StartQuestsUseCase : IGameStartHandler
    {
        private readonly IQuestsRepository _repository;
        private readonly QuestsCollection _quests;
        private readonly CurrentQuestReporter _reporter;

        public StartQuestsUseCase(IQuestsRepository repository, QuestsCollection quests, CurrentQuestReporter reporter)
        {
            _repository = repository;
            _quests = quests;
            _reporter = reporter;
        }

        public void OnGameStarted()
        {
            foreach (var quest in _repository.LoadQuests())
                _quests.Add(quest);

            _quests.Select(_repository.FirstQuestName);
            _reporter.Report();
        }
    }
}

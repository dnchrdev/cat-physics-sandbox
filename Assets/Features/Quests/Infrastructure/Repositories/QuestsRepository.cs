using System.Collections.Generic;
using Features.Quests.Entities;
using Features.Quests.Infrastructure.Config;
using Features.Quests.UseCases.Ports;
using YG;

namespace Features.Quests.Infrastructure.Repositories
{
    public sealed class QuestsRepository : IQuestsRepository
    {
        private readonly QuestsConfig _config;
        private readonly QuestsRefs _refs;

        public QuestsRepository(QuestsConfig config, QuestsRefs refs)
        {
            _config = config;
            _refs = refs;
        }

        public string FirstQuestName => _config.FirstQuestName;

        public IReadOnlyList<Quest> LoadQuests()
        {
            var behaviours = _refs.GetQuests();
            var quests = new List<Quest>(behaviours.Length);

            foreach (var behaviour in behaviours)
            {
                var data = _config.GetQuest(behaviour.QuestName);
                var description = YG2.lang == "ru" ? data.DescriptionRU : data.DescriptionENG;

                quests.Add(new Quest(
                    data.Name,
                    description,
                    data.TargetProgress,
                    behaviour.TargetCount,
                    behaviour.CreateObjective()));
            }

            return quests;
        }
    }
}

using System.Collections.Generic;
using Features.Quests.Entities;

namespace Features.Quests.UseCases.Ports
{
    public interface IQuestsRepository
    {
        string FirstQuestName { get; }
        IReadOnlyList<Quest> LoadQuests();
    }
}

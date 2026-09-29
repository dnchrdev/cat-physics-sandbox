using System.Collections.Generic;

namespace Features.Quests.UseCases.Ports
{
    public interface IQuestListOutput
    {
        void ShowQuestList(IReadOnlyList<QuestSummary> quests);
        void HideQuestList();
    }
}

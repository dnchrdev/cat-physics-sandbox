using System.Collections.Generic;

namespace Features.Quests.UseCases.Ports
{
    public interface IQuestHintsOutput
    {
        void ShowHints(string questName, IReadOnlyList<int> targetIndices);
        void HideHints();
    }
}

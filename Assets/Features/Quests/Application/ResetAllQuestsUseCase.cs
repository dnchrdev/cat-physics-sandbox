using Features.Quests.Domain;
using Zenject;

namespace Features.Quests.Application
{
    public class ResetAllQuestsUseCase
    {
        [Inject] QuestsCollection _questsCollection;

        public void ResetAllQuests()
        {
            _questsCollection.ResetAllQuests();
        }
    }
}
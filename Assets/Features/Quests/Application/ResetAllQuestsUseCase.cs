using Zenject;

namespace Feature.Quests
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
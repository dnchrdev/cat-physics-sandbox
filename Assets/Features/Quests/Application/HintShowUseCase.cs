using Features.Quests.Infrastructure;

namespace Features.Quests.Application
{
    public class HintShowUseCase
    {
        private QuestTipsManager _questTipsManager;

        public HintShowUseCase(QuestTipsManager questTipsManager)
        {
            _questTipsManager = questTipsManager;
        }

        public void ShowTip(string name)
        {

        }
    }
}
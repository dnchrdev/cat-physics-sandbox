using Features.Quests.Entities;
using Features.Shared.UseCases;

namespace Features.Quests.UseCases
{
    public sealed class RequestQuestHintsUseCase
    {
        private readonly QuestsCollection _quests;
        private readonly IRewardedAdvRequest _rewardedAdv;
        private readonly CurrentQuestReporter _reporter;

        public RequestQuestHintsUseCase(
            QuestsCollection quests,
            IRewardedAdvRequest rewardedAdv,
            CurrentQuestReporter reporter)
        {
            _quests = quests;
            _rewardedAdv = rewardedAdv;
            _reporter = reporter;
        }

        public void Execute()
        {
            var quest = _quests.Current;
            if (quest == null || quest.IsCompleted || quest.IsHintsVisible)
                return;

            _rewardedAdv.ShowRewarded(() =>
            {
                if (quest.TryShowHints() && _quests.IsCurrent(quest))
                    _reporter.Report();
            });
        }
    }
}

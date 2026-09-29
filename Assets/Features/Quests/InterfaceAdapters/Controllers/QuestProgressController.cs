using System;
using System.Collections.Generic;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.UseCases;
using Features.Shared.Entities.Ports;
using Zenject;

namespace Features.Quests.InterfaceAdapters.Controllers
{
    public sealed class QuestProgressController : IInitializable, IDisposable
    {
        private readonly List<IQuestSource> _sources;
        private readonly IPlayerLifeEvents _playerLifeEvents;
        private readonly RecordQuestProgressUseCase _recordProgress;
        private readonly ResetAllQuestsUseCase _resetAll;

        public QuestProgressController(
            List<IQuestSource> sources,
            IPlayerLifeEvents playerLifeEvents,
            RecordQuestProgressUseCase recordProgress,
            ResetAllQuestsUseCase resetAll)
        {
            _sources = sources;
            _playerLifeEvents = playerLifeEvents;
            _recordProgress = recordProgress;
            _resetAll = resetAll;
        }

        public void Initialize()
        {
            foreach (var source in _sources)
            {
                source.TargetEventOccurred += _recordProgress.Execute;
                source.StartReporting();
            }

            _playerLifeEvents.Respawned += _resetAll.Execute;
        }

        public void Dispose()
        {
            foreach (var source in _sources)
            {
                source.StopReporting();
                source.TargetEventOccurred -= _recordProgress.Execute;
            }

            _playerLifeEvents.Respawned -= _resetAll.Execute;
        }
    }
}

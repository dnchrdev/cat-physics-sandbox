using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;
using Features.Tutorial.Entities;
using Features.Tutorial.UseCases.Ports;

namespace Features.Tutorial.UseCases
{
    public sealed class TutorialCompletedChecker
    {
        private readonly IReadOnlyPlayer _player;
        private readonly IUIPanelsManager _panels;
        private readonly ITutorialFinishZoneProvider _finishZone;

        private bool _isCompleted;

        public TutorialCompletedChecker(
            IReadOnlyPlayer player,
            IUIPanelsManager panels,
            ITutorialFinishZoneProvider finishZone)
        {
            _player = player;
            _panels = panels;
            _finishZone = finishZone;
        }

        public void Tick()
        {
            if (_isCompleted)
                return;

            if (!TutorialZoneCalculator.IsInsideZone(_player.Position, _finishZone.FinishAreaCenter, _finishZone.FinishAreaRadius))
                return;

            _isCompleted = true;
            _panels.OpenPanel(PanelMode.TutorialCompleted);
        }
    }
}

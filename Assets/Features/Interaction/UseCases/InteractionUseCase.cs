using Features.Interaction.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Interaction.UseCases
{
    public sealed class InteractionUseCase
    {
        private readonly FocusUseCase _focus;
        private readonly GrabUseCase _grab;
        private readonly IInteractionConfig _config;
        private readonly IInteractionOutputBoundary _output;

        public InteractionUseCase(
            FocusUseCase focus,
            GrabUseCase grab,
            IInteractionConfig config,
            IInteractionOutputBoundary output)
        {
            _focus = focus;
            _grab = grab;
            _config = config;
            _output = output;
        }

        public void Execute(InteractionRequest request, IEntity player, float deltaTime)
        {
            HandleRequest(request, player);

            if (_grab.IsHolding)
            {
                _grab.Tick(deltaTime);
                if (_grab.ShouldForceRelease())
                    ForceRelease();
            }
            else
            {
                _focus.Tick();
            }
        }

        public void HandleKnockout() => ForceRelease();

        private void HandleRequest(InteractionRequest request, IEntity player)
        {
            if (_grab.IsHolding)
            {
                if (request.Throw) _grab.Throw();
                else if (request.Release) _grab.Release(forced: false);
            }
            else
            {
                if (request.Grab) TryGrab(player);
                else if (request.Hit) _focus.Hit(_config.FocusHitPower, player);
            }
        }

        private void TryGrab(IEntity player)
        {
            var target = _focus.Target;
            if (!_grab.TryGrab(target, _focus.FocusPoint, player)) return;

            _focus.Clear();

            if (target.ShowsGrabPoint)
                _output.ShowIndication(IndicationType.Grab);
            _output.ShowAfterGrabHint();
        }

        private void ForceRelease()
        {
            _grab.Release(forced: true);
            _focus.Clear();
        }
    }
}

using Features.Interaction.Entities;
using Features.Interaction.InterfaceAdapters.Ports;
using Features.Interaction.UseCases.Ports;
using Features.Shared.Entities;
using Features.Storage.Entities.Ports;
using UnityEngine;
using Zenject;

namespace Features.Interaction.InterfaceAdapters.Presenters
{
    public sealed class InteractionPresenter : IInitializable, IInteractionOutputBoundary
    {
        private readonly IInteractionHintsShower _hints;
        private readonly IInteractionIndicator _indicator;
        private readonly IScreenProjection _screenProjection;
        private readonly IButtonVisibilityWriter _buttons;

        public InteractionPresenter(
            IInteractionHintsShower hints,
            IInteractionIndicator indicator,
            IScreenProjection screenProjection,
            IButtonVisibilityWriter buttons)
        {
            _hints = hints;
            _indicator = indicator;
            _screenProjection = screenProjection;
            _buttons = buttons;
        }

        public void Initialize() => CloseHints();

        public void ShowHint(InteractionType type)
        {
            _hints.ShowHint(type);
            SetButtons(
                hit: type is InteractionType.Hit or InteractionType.Full,
                grab: type is InteractionType.Grab or InteractionType.Full,
                @throw: false,
                release: false);
        }

        public void ShowAfterGrabHint()
        {
            _hints.ShowAfterGrabHint();
            SetButtons(hit: false, grab: false, @throw: true, release: true);
        }

        public void CloseHints()
        {
            _hints.CloseAllHints();
            SetButtons(hit: false, grab: false, @throw: false, release: false);
        }

        public void ShowIndication(IndicationType type)
        {
            _indicator.ShowIndication();
            if (type == IndicationType.Focus)
                _indicator.ShowFocusedIndication();
            else
                _indicator.ShowGrabbedIndication();
        }

        public void MoveIndication(Vector3 worldPoint)
        {
            if (!_screenProjection.IsFacingPoint(worldPoint)) return;
            _indicator.UpdateIndicationPosition(_screenProjection.WorldToScreenPoint(worldPoint));
        }

        public void HideIndication() => _indicator.HideIndication();

        private void SetButtons(bool hit, bool grab, bool @throw, bool release)
        {
            _buttons.SetVisible(AdjustableButtonType.Hit, hit);
            _buttons.SetVisible(AdjustableButtonType.Grab, grab);
            _buttons.SetVisible(AdjustableButtonType.Throw, @throw);
            _buttons.SetVisible(AdjustableButtonType.Release, release);
        }
    }
}

using Features.MobileButtonsAdjustment.InterfaceAdapters.Ports;
using Features.MobileButtonsAdjustment.UseCases.Ports;
using Features.Shared.Entities;
using UnityEngine;

namespace Features.MobileButtonsAdjustment.InterfaceAdapters.Presenters
{
    public sealed class MobileButtonsAdjustmentPresenter : IButtonsAdjustmentOutput
    {
        private readonly IMobileButtonsAdjustmentView _view;

        public MobileButtonsAdjustmentPresenter(IMobileButtonsAdjustmentView view)
        {
            _view = view;
        }

        public void ShowButtonVisibility(AdjustableButtonType button, bool isVisible) =>
            _view.SetButtonVisible(button, isVisible);

        public void ShowCustomPosition(AdjustableButtonType button, Vector2 position) =>
            _view.SetButtonPosition(button, position);

        public void ShowDefaultPosition(AdjustableButtonType button) =>
            _view.RestoreDefaultPosition(button);
    }
}

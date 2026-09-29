using System;
using Features.MobileButtonsAdjustment.InterfaceAdapters.Ports;
using Features.MobileButtonsAdjustment.UseCases;
using Zenject;

namespace Features.MobileButtonsAdjustment.InterfaceAdapters.Controllers
{
    public sealed class MobileButtonsAdjustmentController : IInitializable, IDisposable
    {
        private readonly IMobileButtonsAdjustmentView _view;
        private readonly OpenButtonsAdjustmentUseCase _open;
        private readonly MoveButtonUseCase _move;
        private readonly ResetButtonPositionsUseCase _reset;
        private readonly ClosePanelUseCase _close;

        public MobileButtonsAdjustmentController(
            IMobileButtonsAdjustmentView view,
            OpenButtonsAdjustmentUseCase open,
            MoveButtonUseCase move,
            ResetButtonPositionsUseCase reset,
            ClosePanelUseCase close)
        {
            _view = view;
            _open = open;
            _move = move;
            _reset = reset;
            _close = close;
        }

        public void Initialize()
        {
            _view.Opened += _open.Execute;
            _view.ButtonDragged += _move.Execute;
            _view.ResetRequested += _reset.Execute;
            _view.CloseRequested += _close.Execute;
        }

        public void Dispose()
        {
            _view.Opened -= _open.Execute;
            _view.ButtonDragged -= _move.Execute;
            _view.ResetRequested -= _reset.Execute;
            _view.CloseRequested -= _close.Execute;
        }
    }
}

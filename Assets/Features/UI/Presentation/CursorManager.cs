using Features.Core.Infrastructure.Services;
using Features.UI.Infrastructure.Data;
using UnityEngine;
using Zenject;

namespace Features.UI.Presentation
{
    public class CursorManager
    {
        private GamePauseService _gamePauseService;

        [Inject]
        private void Construct(GamePauseService gamePauseService)
        {
            _gamePauseService = gamePauseService;
        }

        public void ApplyState(PanelState state)
        {
            Cursor.visible = state.IsCursorVisible;
            Cursor.lockState = state.LockState;

            if (state.IsPaused)
                _gamePauseService.SetPause();
            else
                _gamePauseService.SetPlay();
        }
    }
}
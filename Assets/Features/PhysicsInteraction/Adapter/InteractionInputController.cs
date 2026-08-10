using Feature.Core;
using Feature.CameraFeature;
using Feature.Input;
using Feature.UI;
using Feature.PlayerFeature;
using System;
using UnityEngine;
using Zenject;

namespace Feature.PhysicsInteraction
{
    public class InteractionInputController : IInitializable, IDisposable, ITickable
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly IInteractionInput _input;
        [Inject] private readonly ILogger _logger;
        [Inject] private readonly InteractableHandRig _handRig;
        [Inject] private readonly UIPanelsManager _gameplayPanelManager;
        [Inject] private readonly InteractionIndication _interactionIndication;
        [Inject] private readonly InteractionHintsShower _tipsShower;
        [Inject] private readonly InteractionControllerConfig _config;
        [Inject] private readonly IReadOnlyCamera _camera;

        private InteractionDetector _interactionDetector;
        private InteractableOnFocusHandler _focusHandler;
        private GrabbedInteractableHandler _grabHandler;

        public void Initialize()
        {
            _interactionDetector = new InteractionDetector(_camera, _config);
            _focusHandler = new InteractableOnFocusHandler(_tipsShower, _interactionIndication, _interactionDetector);
            _grabHandler = new GrabbedInteractableHandler(_tipsShower, _handRig, _camera, _config, _interactionIndication, _interactionDetector);

            _player.Knockouted += HandleKnockout;
        }

        public void Dispose()
        {
            _gameplayPanelManager.RemovePanel(_tipsShower);
            _gameplayPanelManager.RemovePanel(_interactionIndication);
            _player.Knockouted -= HandleKnockout;
        }

        public void Tick()
        {
            if (!_player.IsAlive) return;

            HandleInput();

            if (_grabHandler.HasGrabbed)
            {
                _grabHandler.Tick(Time.deltaTime);

                if (_grabHandler.ShouldRelease())
                    ForceRelease();
            }
            else
            {
                
                _focusHandler.Tick(Time.deltaTime);
            }
        }

        private void HandleInput()
        {
            if (!_grabHandler.HasGrabbed)
            {
                if (_input.IsGrab) Grab();
                else if (_input.IsHit) _focusHandler.Hit(_config.FocusHitPower, _player);
            }
            else
            {
                if (_input.IsThrow) _grabHandler.Throw();
                else if (_input.IsRelease) _grabHandler.TryRelease(forced: false);
            }
        }

        private void Grab()
        {
            bool success = _grabHandler.TryGrab(_focusHandler.Focused, _focusHandler.TrackedCollider, _player);

            if (success)
            {
                _focusHandler.ClearFocus();
                _grabHandler.ShowVisualOnGrab();
                _tipsShower.ShowAfterGrabHint();
            }
        }

        private void ForceRelease()
        {
            _grabHandler.TryRelease(forced: true);
            _focusHandler.ClearFocus();
            _interactionIndication.HideIndication();
        }

        private void HandleKnockout()
        {
            ForceRelease();
            _interactionIndication.HideIndication();
        }

        private void LogIfFailed(Result result)
        {
            if (!result.IsSuccess)
                _logger.LogError(GetType(), result.Message);
        }
    }
}

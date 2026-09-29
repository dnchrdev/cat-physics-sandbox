using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Shared.UseCases;
using Features.UI.Infrastructure;
using Features.UI.InterfaceAdapters.Ports;
using Zenject;

namespace Features.UI.InterfaceAdapters
{ 
    public class LoadingScreenService : ILoadingScreenService, IInitializable, IDisposable
    {
        private const float FadeDurationSeconds = 1f;
        private const float RotationSpeedDegreesPerSecond = 180f;

        private readonly UIAnimator _animator;
        private readonly ILoadingScreenView _loadingScreenView;
        private readonly List<Tween> _tweens = new();

        public LoadingScreenService(UIAnimator animator, ILoadingScreenView loadingScreenView)
        {
            _animator = animator;
            _loadingScreenView = loadingScreenView;
        }

        public void Initialize()
        {
            _loadingScreenView.ShowLoadingPanel(false);
            _tweens.Clear();
        }

        public void Dispose()
        {
            KillAllAnimationTweens();
        }

        public async UniTask FadeInAsync()
        {
            KillAllAnimationTweens();
            _loadingScreenView.ShowLoadingPanel(true);
            _loadingScreenView.SetLoadingScreenAlpha(0f);
            _loadingScreenView.SetLoadingCircleAlpha(0f);

            var loadingCircleAlphaTween = _animator.AnimateFromTo(
                from: 0f,
                to: 1f,
                alpha => _loadingScreenView.SetLoadingCircleAlpha(alpha),
                duration: FadeDurationSeconds
            );
            _tweens.Add(loadingCircleAlphaTween);

            var loadingCircleZAngleTween = _animator.AnimateFromToBySpeed(
                from: 0f,
                to: -360f,
                angle => _loadingScreenView.SetLoadingCircleZAngle(angle),
                speed: RotationSpeedDegreesPerSecond,
                duration: 0f
            );
            _tweens.Add(loadingCircleZAngleTween);

            await _animator.AnimateFromTo(
                from: 0f,
                to: 1f,
                alpha => _loadingScreenView.SetLoadingScreenAlpha(alpha),
                duration: FadeDurationSeconds
            ).AsyncWaitForCompletion();
        }

        public async UniTask FadeOutAsync()
        {
            KillAllAnimationTweens();
            _loadingScreenView.ShowLoadingPanel(true);
            _loadingScreenView.SetLoadingScreenAlpha(0f);
            _loadingScreenView.SetLoadingCircleAlpha(0f);

            var loadingCircleAlphaTween = _animator.AnimateFromTo(
                from: 1f,
                to: 0f,
                alpha => _loadingScreenView.SetLoadingCircleAlpha(alpha),
                duration: FadeDurationSeconds
            );
            _tweens.Add(loadingCircleAlphaTween);

            float fromZ = _loadingScreenView.GetLoadingCircleZAngle();
            var loadingCircleZAngleTween = _animator.AnimateFromToBySpeed(
                from: fromZ,
                to: fromZ - 360f,
                angle => _loadingScreenView.SetLoadingCircleZAngle(angle),
                speed: RotationSpeedDegreesPerSecond,
                duration: 0f
            );
            _tweens.Add(loadingCircleZAngleTween);

            await _animator.AnimateFromTo(
                from: 1f,
                to: 0f,
                alpha => _loadingScreenView.SetLoadingScreenAlpha(alpha),
                duration: FadeDurationSeconds
            ).AsyncWaitForCompletion();

            _loadingScreenView.ShowLoadingPanel(false);
            KillAllAnimationTweens();
        }

        private void KillAllAnimationTweens()
        {
            foreach (var t in _tweens)
                t.Kill();
            _tweens.Clear();
        }
    }
}

using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Zenject;

namespace Feature.UI
{
    public class LoadingScreenService : ILoadingScreenService, IInitializable, IDisposable
    {
        private UIAnimator _animator;
        private readonly List<Tween> _tweens = new();
        private ILoadingScreen _loadingScreen;

        public LoadingScreenService(UIAnimator animator, ILoadingScreen loadingScreen)
        {
            _animator = animator;
            _loadingScreen = loadingScreen;
        }

        public void Initialize()
        {
            _loadingScreen.ShowLoadingPanel(false);
            _tweens.Clear();
        }

        public void Dispose()
        {
            KillAllAnimationTweens();
        }

        private void KillAllAnimationTweens()
        {
            foreach (var t in _tweens)
                t.Kill();
            _tweens.Clear();
        }

        public async UniTask StartLoadingAsync()
        {
            KillAllAnimationTweens();

            _loadingScreen.ShowLoadingPanel(true);
            _loadingScreen.SetLoadingScreenAlpha(0f);
            _loadingScreen.SetLoadingCircleAlpha(0f);

            var loadingCircleAlphaTween = _animator.AnimateFromTo(
                from: 0f,
                to: 1f,
                alpha => _loadingScreen.SetLoadingCircleAlpha(alpha),
                duration: 1f
            );

            _tweens.Add(loadingCircleAlphaTween);

            var loadingCircleZAngleTween = _animator.AnimateFromTo(
                from: 0f,
                to: 2880f,
                angle => _loadingScreen.SetLoadingCircleZAngle(angle),
                duration: 15f
            );

            _tweens.Add(loadingCircleZAngleTween);

            await _animator.AnimateFromTo(
                from: 0f,
                to: 1f,
                alpha => _loadingScreen.SetLoadingScreenAlpha(alpha),
                duration: 1f
            ).AsyncWaitForCompletion();
        }

        public async UniTask EndLoadingAsync()
        {
            KillAllAnimationTweens();

            _loadingScreen.ShowLoadingPanel(true);
            _loadingScreen.SetLoadingScreenAlpha(0f);
            _loadingScreen.SetLoadingCircleAlpha(0f);

            var loadingCircleAlphaTween = _animator.AnimateFromTo(
                from: 1f,
                to: 0f,
                alpha => _loadingScreen.SetLoadingCircleAlpha(alpha),
                duration: 1f
            );

            _tweens.Add(loadingCircleAlphaTween);

            float fromZ = _loadingScreen.GetLoadingCircleZAngle();

            var loadingCircleZAngleTween = _animator.AnimateFromTo(
                from: fromZ,
                to: fromZ + 2880f,
                angle => _loadingScreen.SetLoadingCircleZAngle(angle),
                duration: 15f
            );

            _tweens.Add(loadingCircleZAngleTween);

            await _animator.AnimateFromTo(
                from: 1f,
                to: 0f,
                alpha => _loadingScreen.SetLoadingScreenAlpha(alpha),
                duration: 1f
            ).AsyncWaitForCompletion();

            KillAllAnimationTweens();
        }
    }
}

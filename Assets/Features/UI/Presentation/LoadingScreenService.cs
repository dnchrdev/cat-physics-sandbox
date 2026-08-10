using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Feature.UI
{
    public class LoadingScreenService : ILoadingScreenService, IInitializable, IDisposable
    {
        [Inject] private readonly UIAnimator _animator;
        [Inject] private readonly ILoadingScreenView _loadingScreenView;

        private readonly List<Tween> _tweens = new();

        public void Initialize()
        {
            _loadingScreenView.ShowLoadingPanel(false);
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

        public async UniTask FadeInAsync()
        {
            _loadingScreenView.ShowLoadingPanel(true);
            KillAllAnimationTweens();

            _loadingScreenView.ShowLoadingPanel(true);
            _loadingScreenView.SetLoadingScreenAlpha(0f);
            _loadingScreenView.SetLoadingCircleAlpha(0f);

            var loadingCircleAlphaTween = _animator.AnimateFromTo(
                from: 0f,
                to: 1f,
                alpha => _loadingScreenView.SetLoadingCircleAlpha(alpha),
                duration: 1f
            );

            _tweens.Add(loadingCircleAlphaTween);

            var loadingCircleZAngleTween = _animator.AnimateFromToBySpeed(
                from: 0f,
                to: -360f,
                angle => _loadingScreenView.SetLoadingCircleZAngle(angle),
                speed: 180f,
                duration: 0f
            );

            _tweens.Add(loadingCircleZAngleTween);

            await _animator.AnimateFromTo(
                from: 0f,
                to: 1f,
                alpha => _loadingScreenView.SetLoadingScreenAlpha(alpha),
                duration: 1f
            ).AsyncWaitForCompletion();
        }

        public async UniTask FadeOutAsync()
        {
            Debug.Log("FadeOut");
            
            KillAllAnimationTweens();

            _loadingScreenView.ShowLoadingPanel(true);
            _loadingScreenView.SetLoadingScreenAlpha(0f);
            _loadingScreenView.SetLoadingCircleAlpha(0f);

            var loadingCircleAlphaTween = _animator.AnimateFromTo(
                from: 1f,
                to: 0f,
                alpha => _loadingScreenView.SetLoadingCircleAlpha(alpha),
                duration: 1f
            );

            _tweens.Add(loadingCircleAlphaTween);

            float fromZ = _loadingScreenView.GetLoadingCircleZAngle();

            var loadingCircleZAngleTween = _animator.AnimateFromToBySpeed(
                from: fromZ,
                to: fromZ - 360f,
                angle => _loadingScreenView.SetLoadingCircleZAngle(angle),
                speed: 180f,
                duration: 0f
            );

            _tweens.Add(loadingCircleZAngleTween);

            await _animator.AnimateFromTo(
                from: 1f,
                to: 0f,
                alpha => _loadingScreenView.SetLoadingScreenAlpha(alpha),
                duration: 1f
            ).AsyncWaitForCompletion();
            
            _loadingScreenView.ShowLoadingPanel(false);
            KillAllAnimationTweens();
        }
    }
}
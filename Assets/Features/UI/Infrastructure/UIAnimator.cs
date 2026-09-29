using System;
using DG.Tweening;
using UnityEngine;

namespace Features.UI.Infrastructure
{
    public class UIAnimator
    {
        public Tween AnimateFromTo(float from, float to, Action<float> onUpdate, float duration = 1f)
        {
            return DOTween.To(() => from, x => from = x, to, duration)
                .OnUpdate(() => onUpdate(from))
                .SetEase(Ease.Linear)
                .SetUpdate(true);
        }

        public Tween AnimateFromToBySpeed(float from, float to, Action<float> onUpdate, float speed, float duration = 0f)
        {
            float distance = Mathf.Abs(to - from);
            float loopDuration = speed > 0f ? distance / speed : 0f;
            var tween = DOTween.To(() => from, x => from = x, to, loopDuration)
                .OnUpdate(() => onUpdate(from))
                .SetEase(Ease.Linear)
                .SetUpdate(true);
            if (duration <= 0f)
            {
                tween.SetLoops(-1, LoopType.Restart);
            }
            else
            {
                int loopsCount = Mathf.CeilToInt(duration / loopDuration);
                tween.SetLoops(loopsCount, LoopType.Restart);
            }
            return tween;
        }
    }
}

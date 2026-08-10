using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace Feature.UI
{
    public class UIAnimator
    {
        public Tween AnimateFromTo(float from, float to, Action<float> onUpdate, float duration = 1f)
        {
            return DOTween.To(() => from, x => from = x, to, duration)
                .OnUpdate(() => onUpdate(from))
                .SetEase(Ease.Linear);
        }

        public Tween AnimateFromToBySpeed(float from, float to, Action<float> onUpdate, float speed, float duration = 0f)
        {
            float distance = Mathf.Abs(to - from);
            float loopDuration = speed > 0f ? distance / speed : 0f;

            var tween = DOTween.To(() => from, x => from = x, to, loopDuration)
                .OnUpdate(() => onUpdate(from))
                .SetEase(Ease.Linear);

            if (duration <= 0f)
            {
                // бесконечный цикл: from -> to -> from -> to ...
                tween.SetLoops(-1, LoopType.Restart);
            }
            else
            {
                // ограничиваем по общей длительности через таймер, либо через SetLoops с подсчётом
                int loopsCount = Mathf.CeilToInt(duration / loopDuration);
                tween.SetLoops(loopsCount, LoopType.Restart);
            }

            return tween;
        }
    }
}
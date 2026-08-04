using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

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
    }
}
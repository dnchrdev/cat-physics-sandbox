using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Quests.Infrastructure.Factories;
using Features.Quests.Infrastructure.Views;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Shared.Entities.Ports;
using Features.Shared.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.Quests.Infrastructure.Managers
{
    public sealed class QuestHintsManager : IQuestHintsView, ITickable, IDisposable
    {
        private readonly QuestHintsView _panel;
        private readonly IReadOnlyCameraTransform _cameraTransform;
        private readonly IReadOnlyCameraPhysical _cameraPhysical;
        private readonly HintObjectFactory _factory;

        private readonly List<GameObject> _visibleTargetHints = new();
        private readonly List<GameObject> _hiddenTargetHints = new();
        private readonly List<GameObject> _visibleTipHints = new();
        private readonly List<GameObject> _hiddenTipHints = new();
        private readonly List<(IHintAnchor anchor, GameObject hint)> _anchoredHints = new();
        private CancellationTokenSource _cts;

        public QuestHintsManager(
            QuestHintsView panel,
            IReadOnlyCameraTransform cameraTransform,
            IReadOnlyCameraPhysical cameraPhysical,
            HintObjectFactory factory)
        {
            _panel = panel;
            _cameraTransform = cameraTransform;
            _cameraPhysical = cameraPhysical;
            _factory = factory;
        }

        public void Show(IReadOnlyList<IHintAnchor> targets, IReadOnlyList<IHintAnchor> tips)
        {
            Hide();
            _cts = new CancellationTokenSource();
            ShowAsync(targets, tips, _cts.Token).Forget();
        }

        public void Hide()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            MoveToHidden(_visibleTargetHints, _hiddenTargetHints);
            MoveToHidden(_visibleTipHints, _hiddenTipHints);
            _anchoredHints.Clear();
        }

        public void Tick()
        {
            foreach (var (anchor, hint) in _anchoredHints)
            {
                var worldPosition = anchor.WorldPosition;
                var screenPosition = _cameraPhysical.Camera.WorldToScreenPoint(worldPosition);
                var isVisible = screenPosition.z > 0 &&
                                Vector3.Angle(_cameraTransform.Forward, worldPosition - _cameraTransform.Position) < 90f;

                hint.SetActive(isVisible);
                if (isVisible)
                    hint.transform.position = screenPosition;
            }
        }

        private async UniTaskVoid ShowAsync(IReadOnlyList<IHintAnchor> targets, IReadOnlyList<IHintAnchor> tips, CancellationToken ct)
        {
            foreach (var target in targets)
            {
                if (!await TryPlaceHint(target, _hiddenTargetHints, _visibleTargetHints, isTarget: true, ct))
                    return;
            }

            foreach (var tip in tips)
            {
                if (!await TryPlaceHint(tip, _hiddenTipHints, _visibleTipHints, isTarget: false, ct))
                    return;
            }
        }

        private async UniTask<bool> TryPlaceHint(
            IHintAnchor anchor,
            List<GameObject> hiddenPool,
            List<GameObject> visible,
            bool isTarget,
            CancellationToken ct)
        {
            var hint = await GetOrCreateHint(hiddenPool, isTarget, ct);

            if (ct.IsCancellationRequested)
            {
                hint.transform.SetParent(_panel.HiddenHintParent);
                hiddenPool.Add(hint);
                return false;
            }

            visible.Add(hint);
            _anchoredHints.Add((anchor, hint));
            return true;
        }

        private async UniTask<GameObject> GetOrCreateHint(List<GameObject> hiddenPool, bool isTarget, CancellationToken ct)
        {
            if (hiddenPool.Count == 0)
                return await _factory.CreateHint(_panel.VisibleHintParent, isTarget, ct);

            var hint = hiddenPool[0];
            hiddenPool.RemoveAt(0);
            hint.transform.SetParent(_panel.VisibleHintParent);
            return hint;
        }

        private void MoveToHidden(List<GameObject> visible, List<GameObject> hidden)
        {
            foreach (var hint in visible)
            {
                hint.transform.SetParent(_panel.HiddenHintParent);
                hidden.Add(hint);
            }

            visible.Clear();
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}

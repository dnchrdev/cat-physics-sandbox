using Cysharp.Threading.Tasks;
using Feature.CameraFeature;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Zenject;

namespace Feature.Quests
{
    public class QuestTipsManager: ITickable
    {
        [Inject] private readonly IReadOnlyCamera _readOnlyCamera;
        [Inject] private readonly HintsFactory _hintsFactory;
        
        private RectTransform _rectTransform;

        private List<GameObject> _showedQuestTips = new();
        private List<GameObject> _hiddenQuestTips = new();

        private List<GameObject> _showedAddQuestTips = new();
        private List<GameObject> _hiddenAddQuestTips = new();

        private Dictionary<Transform, GameObject> _targetAndTip = new();

        public QuestTipsManager(IReadOnlyCamera readOnlyCamera, HintsFactory hintsFactory)
        {
            _readOnlyCamera = readOnlyCamera;
            _hintsFactory = hintsFactory;
        }

        public void ShowHints(Quest quest, Transform visibleParent)
        {
            _targetAndTip.Clear();

            if (_rectTransform == null) _rectTransform = (RectTransform)visibleParent;

            ShowTipsAsync(quest, visibleParent).Forget();
        }

        private async UniTask ShowTipsAsync(Quest quest, Transform visibleParent)
        {
            foreach (var target in quest.BaseQuest.GetActiveTargets)
            {
                GameObject tip;

                if (_hiddenQuestTips.Count == 0)
                {
                    var createTip = _hintsFactory.GetHint(visibleParent, true);
                    await createTip;

                    tip = createTip.Result;
                }
                else
                {
                    tip = _hiddenQuestTips[0];
                    tip.transform.parent = visibleParent;
                    _hiddenQuestTips.Remove(tip);
                }



                _showedQuestTips.Add(tip);
                _targetAndTip.Add(target.GetTransform(), tip);
            }

            foreach (var target in quest.BaseQuest.GetAdditionalTips)
            {
                GameObject tip;

                if (_hiddenAddQuestTips.Count == 0)
                {
                    var createTip = _hintsFactory.GetHint(visibleParent, false);
                    await createTip;

                    tip = createTip.Result;
                }
                else
                {
                    tip = _hiddenAddQuestTips[0];
                    tip.transform.parent = visibleParent;
                    _hiddenAddQuestTips.Remove(tip);
                }
                _showedAddQuestTips.Add(tip);
                _targetAndTip.Add(target, tip);
            }
        }

        public void HideTips(Transform hiddenParent)
        {
            foreach (var tip in _showedQuestTips)
            {
                tip.transform.parent = hiddenParent;
                _hiddenQuestTips.Add(tip);
            }

            foreach (var tip in _showedAddQuestTips)
            {
                tip.transform.parent = hiddenParent;
                _hiddenAddQuestTips.Add(tip);
            }

            _targetAndTip.Clear();
            _showedQuestTips.Clear();
            _showedAddQuestTips.Clear();
        }

        public void Tick()
        {
            UpdateTips();
        }

        private void UpdateTips()
        {
            if (_targetAndTip.Count > 0)
            {
                foreach (var targetAndTip in _targetAndTip)
                {
                    if (_rectTransform == null || _readOnlyCamera == null) continue;

                    Vector3 targetPosition = targetAndTip.Key.position;
                    GameObject targetTip = targetAndTip.Value;
                    var pos = _readOnlyCamera.Camera.WorldToScreenPoint(targetPosition);

                    bool isVisible = pos.z > 0 &&
                                     Vector3.Angle(_readOnlyCamera.Forward, targetPosition - _readOnlyCamera.Position) < 90f;

                    targetTip.SetActive(isVisible); // прячем если вне взора

                    if (isVisible)
                    {
                        if (!targetTip.activeSelf) targetTip.SetActive(true);
                        targetTip.transform.position = pos;
                    }
                    else
                    {
                        if (targetTip.activeSelf) targetTip.SetActive(false);
                    }
                }
            }
        }
    }
}
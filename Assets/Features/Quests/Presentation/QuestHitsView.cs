using System.Collections;
using UnityEngine;

namespace Feature.Quests
{
    public class QuestHitsView : MonoBehaviour
    {
        [field: SerializeField] public Transform VisibleTipParent { get; private set; }
        [field: SerializeField] public Transform HiddenTipParent { get; private set; }
    }
}
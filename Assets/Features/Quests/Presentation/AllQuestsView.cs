using Feature.UI;
using System.Collections;
using UnityEngine;

namespace Feature.Quests
{
    public class AllQuestsView : MonoBehaviour
    {
        [field: SerializeField] public Transform ShowedContent { get; private set; }
        [field: SerializeField] public Transform HiddenContent { get; private set; }
        [field: SerializeField] public ImageButton ClosedButton { get; private set; }
    }
}
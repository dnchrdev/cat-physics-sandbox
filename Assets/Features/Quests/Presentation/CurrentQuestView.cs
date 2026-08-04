using Feature.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Quests
{
    public class CurrentQuestView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Description { get; private set; }
        [field: SerializeField] public Image FillImage { get; private set; }
        [field: SerializeField] public ImageButton HintsButton { get; private set; }
        [field: SerializeField] public ImageButton AllQuestsButton { get; private set; }
    }
}
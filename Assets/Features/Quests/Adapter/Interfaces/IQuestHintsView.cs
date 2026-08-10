using Feature.UI;
using UnityEngine;

namespace Feature.Quests
{
    public interface IQuestHintsView
    {
        void SetActive(bool isActive);
        Transform GetVisibleTipParent();
        Transform GetHiddenTipParent();
    }
}
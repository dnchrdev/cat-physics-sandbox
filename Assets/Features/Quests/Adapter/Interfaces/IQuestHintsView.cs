using UnityEngine;

namespace Features.Quests.Adapter.Interfaces
{
    public interface IQuestHintsView
    {
        void SetActive(bool isActive);
        Transform GetVisibleTipParent();
        Transform GetHiddenTipParent();
    }
}
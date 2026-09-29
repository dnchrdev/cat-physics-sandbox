using Features.Quests.Infrastructure.Quests;
using UnityEngine;

namespace Features.Quests.Infrastructure
{
    public class QuestsRefs : MonoBehaviour
    {
        public BaseQuest[] GetQuests() => GetComponents<BaseQuest>();
    }
}

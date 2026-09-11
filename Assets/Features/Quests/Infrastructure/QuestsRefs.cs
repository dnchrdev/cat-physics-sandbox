using System.Collections.Generic;
using System.Linq;
using Features.Quests.Infrastructure.Quests;
using UnityEngine;

namespace Features.Quests.Infrastructure
{
    public class QuestsRefs : MonoBehaviour
    {
        public List<BaseQuest> GetQuests() => gameObject.GetComponents<BaseQuest>().ToList();
    }
}
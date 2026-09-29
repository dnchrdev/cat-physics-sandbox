using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Quests.Infrastructure.Config
{
    [Serializable]
    public struct QuestData
    {
        public string Name;
        public int TargetProgress;
        public string DescriptionRU;
        public string DescriptionENG;
    }

    [CreateAssetMenu(fileName = "QuestsConfig", menuName = "Configs/QuestsConfig")]
    public class QuestsConfig : ScriptableObject
    {
        [field: SerializeField] public string FirstQuestName { get; private set; }
        [SerializeField] private List<QuestData> _quests;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(FirstQuestName))
                Debug.LogWarning($"{name}: first quest name is not set.", this);
        }

        public QuestData GetQuest(string name)
        {
            foreach (var quest in _quests)
            {
                if (quest.Name == name)
                {
                    return quest;
                }
            }
            throw new Exception($"{name} - is an invalid quest name!");
        }
    }
}

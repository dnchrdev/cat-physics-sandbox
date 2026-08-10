using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Quests
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
        [SerializeField] private List<QuestData> _quests;

        public QuestData GetQuest(string name)
        {
            foreach (var quest in _quests) 
            { 
                if(quest.Name == name)
                {
                    return quest;
                }
            }

            throw new System.Exception($"{name} - is an invalid quest name!");

        }
    }
}
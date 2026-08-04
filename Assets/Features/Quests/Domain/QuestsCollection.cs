using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Feature.Quests
{
    public class QuestsCollection
    {
        public event Action<Quest> CurrentQuestUpdated;
        public event Action<Quest> QuestUpdated;
        public event Action<Quest> ShowHintsEvent;

        public Quest Current { get; private set; }

        private List<Quest> _quests = new List<Quest>();

        public IEnumerable<Quest> Quests => _quests;

        public void AddQuest(string name, int targetProgress, string description, BaseQuest baseQuest)
        {
            Debug.Log($"quest {name} was added");
            _quests.Add(new Quest(name, targetProgress, description, baseQuest));
        }
        
        public void AddProgress(string name)
        {
            foreach (var quest in _quests)
            {
                if (quest.Name == name) 
                { 
                    if(quest.TryAddProgress() == false) return;

                    if(quest.Name == Current.Name)
                        CurrentQuestUpdated?.Invoke(Current);

                    return;
                }
            }

            throw new Exception("Invalid quest name");
        }

        public void ShowHints(string name)
        {
            foreach (var quest in _quests)
            {
                if (quest.Name == name)
                {
                    if (quest.TryShowTips() == false) return;

                    ShowHintsEvent?.Invoke(quest);
                    return;
                }
            }

            throw new Exception("Invalid quest name");
        }

        public void SwitchCurrentQuest(string name)
        {
            foreach (var quest in _quests)
            {
                if (quest.Name == name)
                {
                    Current = quest;
                    CurrentQuestUpdated?.Invoke(Current);
                    return;
                }
            }

            throw new Exception("Invalid name");
        }

        public float GetProgressRatio(string name)
        {
            foreach (var quest in _quests)
            {
                if (quest.Name == name)
                {
                    return quest.ProgressRatio;
                }
            }

            throw new Exception("Invalid quest name");
        }

        public BaseQuest GetBaseQuest(string name)
        {
            foreach (var quest in _quests)
            {
                if (quest.Name == name)
                {
                    return quest.BaseQuest;
                }
            }

            throw new Exception("Invalid quest name");
        }

        public bool IsQuestCompleted(string name)
        {
            return _quests.Any(q => q.Name == name && q.IsCompleted);
        }

        public bool IsQuestHintsVisible(string name)
        {
            return _quests.Any(q => q.Name == name && q.IsHintsVisible);
        }
        
        public void ResetAllQuests()
        {
            foreach (var quest in _quests)
            {
                quest.ResetProgress();
                quest.BaseQuest.Reset();
                
                if(quest.Name == Current.Name)
                    CurrentQuestUpdated?.Invoke(Current);
       
            }
        }
        
    }
}
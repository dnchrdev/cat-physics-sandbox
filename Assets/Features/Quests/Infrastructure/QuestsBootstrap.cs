using System;
using System.Collections.Generic;
using Feature.PhysicsInteraction;
using UnityEngine;
using YG;
using Zenject;

namespace Feature.Quests
{
    public class QuestsBootstrap : MonoBehaviour, IInitializable
    {
        [SerializeField] private string _firstQuestName;
        
        [Inject] private EntityBindResolver  _entityBindResolver;
        private QuestsCollection _questsCollection;
        private QuestsConfig _config;
        private List<BaseQuest> _quests;

        [Inject]
        private void Construct(QuestsCollection questsCollection, QuestsConfig questsConfig)
        {
            _questsCollection = questsCollection;
            _config = questsConfig;

            _quests = new List<BaseQuest>();
            _quests.AddRange(gameObject.GetComponents<BaseQuest>());
        }

        public void Initialize()
        {
            foreach (var questBase in _quests)
            {
                questBase.Init(_questsCollection, _entityBindResolver);

                Debug.Log($"questBase.Name = {questBase.Name}");

                var questData = _config.GetQuest(questBase.Name);
                
                string description;
                if (YG2.lang == "ru")
                {
                    description =  questData.DescriptionRU;
                }
                else
                {
                    description =  questData.DescriptionRU;
                }
                
                _questsCollection.AddQuest(questData.Name, questData.TargetProgress, description, questBase);
            }

            if (string.IsNullOrWhiteSpace(_firstQuestName)) throw new Exception("First quest name is invalid");

            _questsCollection.SwitchCurrentQuest(_firstQuestName);
        }
    }
}
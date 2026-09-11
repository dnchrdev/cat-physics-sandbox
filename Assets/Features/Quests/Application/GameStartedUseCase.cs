using System;
using System.Collections.Generic;
using Features.PhysicsInteraction.Adapter.Interaction;
using Features.Quests.Domain;
using Features.Quests.Infrastructure;
using Features.Quests.Infrastructure.Config;
using Features.Quests.Infrastructure.Quests;
using UnityEngine;
using YG;
using Zenject;

namespace Features.Quests.Application
{
    public class GameStartedUseCase
    {
        [Inject] private readonly QuestsRefs _refs;
        [Inject] private EntityBindResolver  _entityBindResolver;
        [Inject] private QuestsCollection _questsCollection;
        [Inject] private QuestsConfig _config;
        
        private List<BaseQuest> _quests;
        
        public void GameStarted()
        {
            _quests = _refs.GetQuests();
            
            foreach (var questBase in _quests)
            {
                questBase.Init(_questsCollection, _entityBindResolver);

                //Debug.Log($"questBase.Name = {questBase.Name}");

                var questData = _config.GetQuest(questBase.Name);
                
                string description;
                
                //Debug.Log($"YG2.lang = {YG2.lang}");
                
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
            
            _questsCollection.SwitchCurrentQuest(_config.FirstQuestName);
        }
    }
}
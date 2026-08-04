using System.Collections;
using UnityEngine;

namespace Feature.Quests
{
    public class HintShowUseCase
    {
        private QuestTipsManager _questTipsManager;

        public HintShowUseCase(QuestTipsManager questTipsManager)
        {
            _questTipsManager = questTipsManager;
        }

        public void ShowTip(string name)
        {

        }
    }
}
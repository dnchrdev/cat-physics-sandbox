using System;
using System.Collections;
using UnityEngine;

namespace Feature.Quests
{
    public class QuestFeatureEventBus
    {
        public Action<string> ShowTipsButtonClicked;
        public Action HideTipsEvent;
    }
}
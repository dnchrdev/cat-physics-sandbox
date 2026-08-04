using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    [Serializable]
    public struct InteractionAndTips
    {
        public InteractionType InteractionType;
        public List<GameObject> TipObj;
    }
}
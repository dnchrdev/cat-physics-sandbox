using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Data
{
    [Serializable]
    public struct InteractionAndHints
    {
        public InteractionType InteractionType;
        public List<GameObject> HintObj;
    }
}
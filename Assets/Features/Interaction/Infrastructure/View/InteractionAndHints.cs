using System;
using System.Collections.Generic;
using Features.Interaction.Entities;
using UnityEngine;

namespace Features.Interaction.Infrastructure.View
{
    [Serializable]
    public struct InteractionAndHints
    {
        public InteractionType InteractionType;
        public List<GameObject> HintObj;
    }
}

using System;
using Features.Shared.Entities;
using UnityEngine.Serialization;

namespace Features.MobileButtonsAdjustment.Infrastructure
{
    [Serializable]
    public struct AdjustableButtonsMap
    {
        public AdjustableButtonType Type;

        [FormerlySerializedAs("Draggablebutton")]
        public DraggableUIElement DraggableButton;
    }
}

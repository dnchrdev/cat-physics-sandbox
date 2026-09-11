using System;
using UnityEngine;

namespace Features.Quests.Infrastructure.Interfaces
{
    public interface IQuestInteractable
    {
        event Action<IQuestInteractable, Collision> QuestColliderHitEvent;
        event Action<IQuestInteractable> QuestHitEvent;
        event Action<IQuestInteractable> QuestThrowEvent;
        Transform GetTransform();
        Rigidbody GetRigidbody();
    }
}

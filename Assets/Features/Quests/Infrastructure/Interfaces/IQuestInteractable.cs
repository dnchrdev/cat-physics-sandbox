using System;
using UnityEngine;

public interface IQuestInteractable
{
    event Action<IQuestInteractable, Collision, Rigidbody> QuestColliderHitEvent;
    event Action<IQuestInteractable> QuestHitEvent;
    event Action<IQuestInteractable> QuestThrowEvent;
    Transform GetTransform();
}

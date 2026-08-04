using Feature.PhysicsInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Feature.Quests
{
    public class BaseQuest : MonoBehaviour, IDisposable
    {
        [field: SerializeField] public string Name { get; private set; }

        [SerializeField] protected List<Interactable> Targets;
        [SerializeField] private List<Transform> _additionalTips;

        private HashSet<IQuestInteractable> _active = new();
        private HashSet<IQuestInteractable> _done = new();

        public IEnumerable<IQuestInteractable> GetActiveTargets => _active;
        public IEnumerable<Transform> GetAdditionalTips => _additionalTips;
        //public IEnumerable<IQuestInteractable> GetDoneTargets => _done;

        protected QuestsCollection QuestsCollection;
        protected EntityBindResolver  EntityBindResolver;

        public virtual void Init(QuestsCollection questsCollection, EntityBindResolver  entityBindResolver)
        {
            QuestsCollection = questsCollection;
            EntityBindResolver = entityBindResolver;

            _active.AddRange(Targets);
            Debug.Log($"GetActiveTargets = {GetActiveTargets.ToArray().Length}");

        }

        public virtual void Dispose()
        {

        }

        protected bool TryToDoneTarget(IQuestInteractable questInteractable)
        {
            if (QuestsCollection.IsQuestCompleted(Name) || _done.Contains(questInteractable)) return false;

            _active.Remove(questInteractable);
            _done.Add(questInteractable);

            return true;
        }

        public void Reset()
        {
            _done.Clear();
            _active.Clear();
            
            _active.AddRange(Targets);
        }
    }
}

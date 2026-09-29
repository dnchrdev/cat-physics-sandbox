using System;
using System.Collections.Generic;

namespace Features.Quests.Entities
{
    public sealed class QuestsCollection
    {
        private readonly List<Quest> _quests = new();
        private readonly Dictionary<string, Quest> _byName = new();

        public IReadOnlyList<Quest> All => _quests;
        public Quest Current { get; private set; }

        public void Add(Quest quest)
        {
            if (!_byName.TryAdd(quest.Name, quest))
                throw new InvalidOperationException($"Duplicate quest: {quest.Name}");

            _quests.Add(quest);
        }

        public bool TryGet(string name, out Quest quest) => _byName.TryGetValue(name, out quest);

        public void Select(string name)
        {
            if (!_byName.TryGetValue(name, out var quest))
                throw new InvalidOperationException($"Quest not found: {name}");

            Current = quest;
        }

        public bool IsCurrent(Quest quest) => Current == quest;

        public void ResetAll()
        {
            foreach (var quest in _quests)
                quest.Reset();
        }
    }
}

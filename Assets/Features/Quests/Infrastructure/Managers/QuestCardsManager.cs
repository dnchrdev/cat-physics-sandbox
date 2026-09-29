using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Quests.Infrastructure.Factories;
using Features.Quests.Infrastructure.Views;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.UseCases.Ports;

namespace Features.Quests.Infrastructure.Managers
{
    public sealed class QuestCardsManager : IQuestCardsView, IDisposable
    {
        public event Action<string> QuestSelected;

        private readonly AllQuestsView _panel;
        private readonly QuestCardFactory _factory;
        private readonly List<QuestCard> _visibleCards = new();
        private readonly List<QuestCard> _hiddenCards = new();
        private CancellationTokenSource _cts;

        public QuestCardsManager(AllQuestsView panel, QuestCardFactory factory)
        {
            _panel = panel;
            _factory = factory;
        }

        public void ShowCards(IReadOnlyList<QuestSummary> quests)
        {
            HideCards();
            _cts = new CancellationTokenSource();
            ShowCardsAsync(quests, _cts.Token).Forget();
        }

        public void HideCards()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            foreach (var card in _visibleCards)
            {
                card.QuestClicked -= OnCardClicked;
                MoveToHidden(card);
            }

            _visibleCards.Clear();
        }

        private async UniTaskVoid ShowCardsAsync(IReadOnlyList<QuestSummary> quests, CancellationToken  ct)
        {
            foreach (var quest in quests)
            {
                var card = await GetOrCreateCard(ct);
                if (card == null)
                    continue;

                if (ct.IsCancellationRequested)
                {
                    MoveToHidden(card);
                    return;
                }


                card.Init(quest.Name, quest.Description, quest.Progress);
                card.QuestClicked += OnCardClicked;
                _visibleCards.Add(card);
            }
        }

        private async UniTask<QuestCard> GetOrCreateCard(CancellationToken ct)
        {
            if (_hiddenCards.Count == 0)
                return await _factory.CreateCard(_panel.VisibleCardParent, ct);

            var card = _hiddenCards[0];
            _hiddenCards.RemoveAt(0);
            card.transform.SetParent(_panel.VisibleCardParent);
            return card;
        }

        private void MoveToHidden(QuestCard card)
        {
            card.transform.SetParent(_panel.HiddenCardParent);
            _hiddenCards.Add(card);
        }

        private void OnCardClicked(string questName) => QuestSelected?.Invoke(questName);
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}
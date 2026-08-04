using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Feature.Quests
{
    public class QuestCaldsManager
    {
        public event Action<string> NewQuestSelected;

        private List<QuestCard> _showedQuestCards = new ();
        private List<QuestCard> _hiddenQuestCards = new ();

        private QuestsCollection _questsCollection;
        private QuestCardFactory _questCardFactory;

        private Dictionary<QuestCard, Action<string>> _cardHandlers = new ();

        public QuestCaldsManager(QuestsCollection questsCollection, QuestCardFactory questCardFactory)
        {
            _questsCollection = questsCollection;
            _questCardFactory = questCardFactory;
        }

        public void CreateAllQuestCards(Transform showed)
        {
            CreateAllQuestCardsAsync(showed).Forget();
        }

        public void HideAllQuestCards(Transform hidden)
        {
            foreach (var card in _showedQuestCards)
            {
                card.transform.parent = hidden;
                _hiddenQuestCards.Add(card);

                if (_cardHandlers.TryGetValue(card, out var handler))
                {
                    card.QuestClicked -= handler;
                    _cardHandlers.Remove(card);
                }
            }

            _showedQuestCards.Clear();
        }

        private async UniTask CreateAllQuestCardsAsync(Transform showed)
        {
            foreach (var quest in _questsCollection.Quests.ToArray())
            {
                QuestCard questCard;
                if(_hiddenQuestCards.Count == 0)
                {
                    var createCard = _questCardFactory.GetQuestCard(showed);
                    await createCard;
                    questCard = createCard.Result;
                }
                else
                {
                    questCard = _hiddenQuestCards[0];
                    questCard.transform.parent = showed;
                    _hiddenQuestCards.Remove(questCard);
                }

                questCard.Init(quest.Name, quest.Description, quest.ProgressRatio);

                Action<string> handler = (name) => CardSelected(name);

                questCard.QuestClicked += handler;

                _cardHandlers.Add(questCard, handler);

                _showedQuestCards.Add(questCard);
            }
        }

        private void CardSelected(string name)
        {
            NewQuestSelected?.Invoke(name);
        }
    }
}
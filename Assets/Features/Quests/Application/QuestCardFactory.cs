using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Features.Quests.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Features.Quests.Application
{
    public class QuestCardFactory
    {
        public async Task<QuestCard> GetQuestCard(Transform parent)
        {
            var cardObj = Addressables.InstantiateAsync("QuestCard", parent);
            await cardObj.ToUniTask();

            if (cardObj.Status is AsyncOperationStatus.Succeeded)
            {
                return cardObj.Result.GetComponent<QuestCard>();
            }

            Debug.LogError("QuestCard instantiate Async went wrong");
            return null;

        }
    }
}
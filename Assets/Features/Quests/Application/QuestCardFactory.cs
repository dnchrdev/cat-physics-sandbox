using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Feature.Quests
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
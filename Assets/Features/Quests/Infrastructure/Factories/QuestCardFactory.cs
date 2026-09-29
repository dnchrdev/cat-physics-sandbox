using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Features.Quests.Infrastructure.Factories
{
    public class QuestCardFactory
    {
        public async UniTask<QuestCard> CreateCard(Transform parent, CancellationToken ct)
        {
            var handle = Addressables.InstantiateAsync("QuestCard", parent);
            
            await handle.ToUniTask(cancellationToken: ct, autoReleaseWhenCanceled: true);  
            
            if (handle.Status == AsyncOperationStatus.Succeeded)
                return handle.Result.GetComponent<QuestCard>();
            
            Debug.LogError("QuestCard instantiate failed");
            return null;
        }
    }
}

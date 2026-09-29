using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Features.Quests.Infrastructure.Factories
{
    public class HintObjectFactory
    {
        public async UniTask<GameObject> CreateHint(Transform parent, bool isTarget,  CancellationToken ct)
        {
            var key = isTarget ? "TargetHint" : "AdditionalHint";
            return await Addressables.InstantiateAsync(key, parent)
                .ToUniTask(cancellationToken: ct, autoReleaseWhenCanceled: true);
        }
    }
}

using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Feature.Quests
{
    public class HintsFactory
    {
        public async Task<GameObject> GetHint(Transform parent, bool isTarget = true)
        {
            var createObj = Addressables.InstantiateAsync(isTarget? "TargetHint": "AdditionalHint", parent);

            await createObj.ToUniTask();

            if (createObj.Status is AsyncOperationStatus.Succeeded)
            {
                return createObj.Result;
            }

            Debug.LogError("Hint instantiate Async went wrong");
            return null;

        }
    }
}
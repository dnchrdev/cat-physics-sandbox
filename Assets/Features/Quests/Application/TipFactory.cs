using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Feature.Quests
{
    public class TipFactory
    {
        public async Task<GameObject> GetTip(Transform parent, bool isTarget = true)
        {
            var createObj = Addressables.InstantiateAsync(isTarget? "TargetTip": "AdditionalTip", parent);

            Debug.Log($"create tip");

            await createObj.ToUniTask();

            if (createObj.Status is AsyncOperationStatus.Succeeded)
            {
                return createObj.Result;
            }

            Debug.LogError("Tip instantiate Async went wrong");
            return null;

        }
    }
}
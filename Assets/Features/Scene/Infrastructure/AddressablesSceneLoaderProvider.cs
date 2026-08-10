using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Feature.Scene
{
    public interface ISceneLoaderProvider
    {
        AsyncOperationHandle<SceneInstance> BeginLoad(SceneLoadData data, bool activateOnLoad);
        UniTask WaitForLoad(AsyncOperationHandle<SceneInstance> handle);
        UniTask ActivateAsync(AsyncOperationHandle<SceneInstance> handle);
        UniTask UnloadAsync(AsyncOperationHandle<SceneInstance> handle);
        bool IsValid(AsyncOperationHandle<SceneInstance> handle);
    }

    public sealed class AddressablesSceneLoaderProvider : ISceneLoaderProvider
    {
        public AsyncOperationHandle<SceneInstance> BeginLoad(SceneLoadData data, bool activateOnLoad) =>
            Addressables.LoadSceneAsync(data.AddressableRef, data.LoadMode,
                activateOnLoad);

        public async UniTask WaitForLoad(AsyncOperationHandle<SceneInstance> handle) =>
            await handle.ToUniTask();

        public async UniTask ActivateAsync(AsyncOperationHandle<SceneInstance> handle) =>
            await handle.Result.ActivateAsync().ToUniTask();

        public async UniTask UnloadAsync(AsyncOperationHandle<SceneInstance> handle) =>
            await Addressables.UnloadSceneAsync(handle).ToUniTask();

        public bool IsValid(AsyncOperationHandle<SceneInstance> handle) => handle.IsValid();
    }
}
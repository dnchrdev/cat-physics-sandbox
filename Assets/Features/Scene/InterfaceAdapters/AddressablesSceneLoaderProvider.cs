using Cysharp.Threading.Tasks;
using Features.Scene.Entities;
using Features.Scene.InterfaceAdapters.Ports;
using Features.Scene.UseCases.Ports;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Features.Scene.InterfaceAdapters
{
    public sealed class AddressablesSceneLoaderProvider : ISceneLoaderProvider
    {
        private readonly IAddressableSceneRefResolver _refResolver;

        public AddressablesSceneLoaderProvider(IAddressableSceneRefResolver refResolver)
        {
            _refResolver = refResolver;
        }

        public ISceneHandle BeginLoad(SceneDescriptor descriptor, bool activateOnLoad)
        {
            var assetRef = _refResolver.Resolve(descriptor.Id);
            var loadMode = descriptor.LoadMode == SceneLoadMode.Additive
                ? LoadSceneMode.Additive
                : LoadSceneMode.Single;

            var handle = Addressables.LoadSceneAsync(assetRef, loadMode, activateOnLoad);
            return new AddressablesSceneHandle(handle);
        }

        public async UniTask WaitForLoad(ISceneHandle handle) =>
            await Unwrap(handle).ToUniTask();

        public async UniTask ActivateAsync(ISceneHandle handle) =>
            await Unwrap(handle).Result.ActivateAsync().ToUniTask();

        public async UniTask UnloadAsync(ISceneHandle handle) =>
            await Addressables.UnloadSceneAsync(Unwrap(handle)).ToUniTask();

        public bool IsValid(ISceneHandle handle) => handle.IsValid;

        private static AsyncOperationHandle<SceneInstance> Unwrap(ISceneHandle handle) =>
            ((AddressablesSceneHandle)handle).Handle;
    }
}

using Features.Scene.UseCases.Ports;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Features.Scene.InterfaceAdapters
{
    public sealed class AddressablesSceneHandle : ISceneHandle
    {
        public AsyncOperationHandle<SceneInstance> Handle { get; }

        public AddressablesSceneHandle(AsyncOperationHandle<SceneInstance> handle)
        {
            Handle = handle;
        }

        public bool IsValid => Handle.IsValid();
        public bool Succeeded => Handle.Status == AsyncOperationStatus.Succeeded;
    }
}

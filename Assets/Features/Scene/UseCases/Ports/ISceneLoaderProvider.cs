using Cysharp.Threading.Tasks;
using Features.Scene.Entities;

namespace Features.Scene.UseCases.Ports
{
    public interface ISceneLoaderProvider
    {
        ISceneHandle BeginLoad(SceneDescriptor descriptor, bool activateOnLoad);
        UniTask WaitForLoad(ISceneHandle handle);
        UniTask ActivateAsync(ISceneHandle handle);
        UniTask UnloadAsync(ISceneHandle handle);
        bool IsValid(ISceneHandle handle);
    }
}

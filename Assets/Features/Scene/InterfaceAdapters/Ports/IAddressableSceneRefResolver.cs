using Features.Shared.Entities;
using UnityEngine.AddressableAssets;

namespace Features.Scene.InterfaceAdapters.Ports
{
    public interface IAddressableSceneRefResolver
    {
        AssetReference Resolve(SceneId id);
    }
}

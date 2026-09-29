using Features.Scene.Entities;
using Features.Scene.InterfaceAdapters.Ports;
using Features.Scene.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Features.Scene.Infrastructure.Configs
{

    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Configs/ScenesConfig")]
    public sealed class ScenesConfig : ScriptableObject, IScenesConfigProvider, IAddressableSceneRefResolver
    {
        [field: SerializeField] public AddressableSceneEntry MainMenu { get; private set; }
        [field: SerializeField] public AddressableSceneEntry Tutorial { get; private set; }
        [field: SerializeField] public AddressableSceneEntry Gameplay { get; private set; }

        public Result<SceneDescriptor> TryGetScene(SceneId id)
        {
            return id switch
            {
                SceneId.MainMenu => Result<SceneDescriptor>.Success(MainMenu.ToDescriptor()),
                SceneId.Tutorial => Result<SceneDescriptor>.Success(Tutorial.ToDescriptor()),
                SceneId.Gameplay => Result<SceneDescriptor>.Success(Gameplay.ToDescriptor()),
                _ => Result<SceneDescriptor>.Failure($"Scene with id '{id}' is not configured in {name}"),
            };
        }

        public AssetReference Resolve(SceneId id)
        {
            return id switch
            {
                SceneId.MainMenu => MainMenu.AddressableRef,
                SceneId.Tutorial => Tutorial.AddressableRef,
                SceneId.Gameplay => Gameplay.AddressableRef,
                _ => null,
            };
        }
    }
}

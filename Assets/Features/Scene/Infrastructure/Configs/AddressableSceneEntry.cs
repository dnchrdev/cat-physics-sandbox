using System;
using Features.Scene.Entities;
using Features.Shared.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Features.Scene.Infrastructure.Configs
{
    [Serializable]
    public struct AddressableSceneEntry
    {
        [field: SerializeField] public SceneId Id { get; private set; }
        [field: SerializeField] public AssetReference AddressableRef { get; private set; }
        [field: SerializeField] public LoadSceneMode LoadMode { get; private set; }

        public SceneDescriptor ToDescriptor() =>
            new(Id, LoadMode == LoadSceneMode.Additive ? SceneLoadMode.Additive : SceneLoadMode.Single);
    }
}

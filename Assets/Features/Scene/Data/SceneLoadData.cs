using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Feature.Scene
{
    [Serializable]
    public struct SceneLoadData
    {
        [field: SerializeField] public SceneId Id { get; private set; }
        [field: SerializeField] public AssetReference AddressableRef { get; private set; }
        [field: SerializeField] public LoadSceneMode LoadMode { get; private set; }
    }
}
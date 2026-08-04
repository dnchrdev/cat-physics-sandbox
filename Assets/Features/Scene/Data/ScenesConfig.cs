using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.Scene
{
    [Serializable]
    public struct SceneAndMode
    {
        [field: SerializeField] public string ScenePath { get; private set; }
        [field: SerializeField] public LoadSceneMode LoadMode { get; private set; }
    }

    [CreateAssetMenu(fileName = "SceneObject", menuName = "SO/SceneObject")]
    public class ScenesConfig : ScriptableObject
    {
        [field: SerializeField] public List<SceneAndMode> _scenes { get; private set; } = new ();

        public SceneAndMode GetScene(string path)
        {
            foreach (var scene in _scenes)
            {
                if (scene.ScenePath == path) return scene;
            }

            throw new Exception("SCENE PATH IS NOT VALID");
        }
    }
}

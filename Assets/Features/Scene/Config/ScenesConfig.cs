using System;
using System.Collections.Generic;
using Feature.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.Scene
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Configs/ScenesConfig")]
    public class ScenesConfig : ScriptableObject
    {
        [field: SerializeField] public SceneLoadData MainMenu { get; private set; }
        [field: SerializeField] public SceneLoadData Tutorial { get; private set; }
        [field: SerializeField] public SceneLoadData Gameplay { get; private set; }

        public Result<SceneLoadData> TryGetScene(SceneId id)
        {
            return id switch
            {
                SceneId.MainMenu => Result<SceneLoadData>.Success(MainMenu), 
                SceneId.Tutorial => Result<SceneLoadData>.Success(Tutorial),
                SceneId.Gameplay => Result<SceneLoadData>.Success(Gameplay),
                _ => Result<SceneLoadData>.Failure("Scene with id '{id}' is not configured in {name}"),
            };
        }
    }
}

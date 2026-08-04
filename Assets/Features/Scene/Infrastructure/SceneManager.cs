using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

using Feature.Core;

namespace Feature.Scene
{
    public class SceneManager
    {
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _inProgress = new();
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _loaded = new();

        public async UniTask<Result> LoadAsync(SceneAndMode scene)
        {
            var path = scene.ScenePath;
            var mode = scene.LoadMode;

            if (_loaded.ContainsKey(path))
            {
                return Result.Failure($"{path} already loaded");
            }

            if (_inProgress.ContainsKey(path))
            {
                return Result.Failure($"{path} already in progress");
            }

            var handle = Addressables.LoadSceneAsync(path, mode);

            _inProgress[path] = handle;

            await handle.ToUniTask();

            _inProgress.Remove(path);

            if (handle.IsValid() && handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loaded[path] = handle;
                return Result.Success();
            }
            else
            {
                return Result.Failure($"{path} failed to load");
            }
        }

        public async UniTask<Result> UnloadAsync(SceneAndMode scene)
        {
            var path = scene.ScenePath;

            if (!_loaded.TryGetValue(path, out var handle))
            {
                return Result.Failure($"{path} not loaded");
            }

            if (_inProgress.ContainsKey(path))
            {
                return Result.Failure($"{path} in progress");
            }

            _inProgress.Add(path, handle);

            await Addressables.UnloadSceneAsync(handle).ToUniTask();

            _inProgress.Remove(path);
            _loaded.Remove(path);
            return Result.Success();
        }

    }
}

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Feature.Core;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace Feature.Scene
{
    internal enum SceneState
    {
        Loading,
        Loaded,
        Unloading
    }

    internal sealed class SceneEntry
    {
        public SceneState State;
        public AsyncOperationHandle<SceneInstance> Handle;
    }

    public sealed class SceneLoadingService
    {
        [Inject] private readonly ScenesConfig _scenesConfig;
        [Inject] private readonly ISceneLoaderProvider _provider;
        private readonly Dictionary<SceneId, SceneEntry> _entries = new();

        public bool IsLoaded(SceneId id) => _entries.TryGetValue(id, out var e) && e.State == SceneState.Loaded;

        public async UniTask<Result> LoadAsync(SceneId id, bool activateImmediately = true)
        {
            if (_entries.ContainsKey(id))
            {
                return Result.Failure($"{id} already loaded or in progress");
            }

            var result = _scenesConfig.TryGetScene(id);
            
            if (result.IsSuccess == false)
            {
                return Result.Failure(result.Message);
            }

            var sceneData = result.Value;
            var handle = _provider.BeginLoad(sceneData , activateImmediately);
            var entry = new SceneEntry { State = SceneState.Loading, Handle = handle };
            _entries[id] = entry;

            await _provider.WaitForLoad(handle);

            if (!_provider.IsValid(handle) || handle.Status != AsyncOperationStatus.Succeeded)
            {
                _entries.Remove(id);
                return Result.Failure($"{id} failed to load");
            }

            if (activateImmediately)
            {
                entry.State = SceneState.Loaded;
            }

            return Result.Success();
        }

        public async UniTask<Result> ActivateAsync(SceneId id)
        {
            if (!_entries.TryGetValue(id, out var entry) || entry.State != SceneState.Loading)
            {
                return Result.Failure($"{id} is not pending activation");
            }

            await _provider.ActivateAsync(entry.Handle);
            entry.State = SceneState.Loaded;
            return Result.Success();
        }

        public async UniTask<Result> UnloadAsync(SceneId id)
        {
            if (!_entries.TryGetValue(id, out var entry) || entry.State != SceneState.Loaded)
            {
                return Result.Failure($"{id} not loaded");
            }

            entry.State = SceneState.Unloading;

            await _provider.UnloadAsync(entry.Handle);

            _entries.Remove(id);
            return Result.Success();
        }
    }
}
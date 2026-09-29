using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.Scene.Entities;
using Features.Scene.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.Shared.Utilities;

namespace Features.Scene.UseCases
{
    public sealed class SceneLoadingService : ISceneLoadingService
    {
        private readonly IScenesConfigProvider _scenesConfigProvider;
        private readonly ISceneLoaderProvider _loaderProvider;

        private readonly Dictionary<SceneId, SceneEntry> _entries = new();

        public SceneLoadingService(IScenesConfigProvider scenesConfigProvider, ISceneLoaderProvider loaderProvider)
        {
            _scenesConfigProvider = scenesConfigProvider;
            _loaderProvider = loaderProvider;
        }

        public bool IsLoaded(SceneId id) =>
            _entries.TryGetValue(id, out var entry) && entry.State == SceneState.Loaded;

        public async UniTask<Result> LoadAsync(SceneId id, bool activateImmediately = true)
        {
            if (_entries.ContainsKey(id))
                return Result.Failure($"{id} already loaded or in progress");

            var configResult = _scenesConfigProvider.TryGetScene(id);
            if (configResult.IsSuccess == false)
                return Result.Failure(configResult.Error);

            var descriptor = configResult.Value;
            var handle = _loaderProvider.BeginLoad(descriptor, activateImmediately);
            var entry = new SceneEntry(SceneState.Loading, handle);
            _entries[id] = entry;

            await _loaderProvider.WaitForLoad(handle);

            if (!_loaderProvider.IsValid(handle) || !handle.Succeeded)
            {
                _entries.Remove(id);
                return Result.Failure($"{id} failed to load");
            }

            if (activateImmediately)
                entry.State = SceneState.Loaded;

            return Result.Success();
        }

        public async UniTask<Result> ActivateAsync(SceneId id)
        {
            if (!_entries.TryGetValue(id, out var entry) || entry.State != SceneState.Loading)
                return Result.Failure($"{id} is not pending activation");

            await _loaderProvider.ActivateAsync(entry.Handle);
            entry.State = SceneState.Loaded;
            return Result.Success();
        }

        public async UniTask<Result> UnloadAsync(SceneId id)
        {
            if (!_entries.TryGetValue(id, out var entry) || entry.State != SceneState.Loaded)
                return Result.Failure($"{id} not loaded");

            entry.State = SceneState.Unloading;
            await _loaderProvider.UnloadAsync(entry.Handle);
            _entries.Remove(id);
            return Result.Success();
        }
    }
}

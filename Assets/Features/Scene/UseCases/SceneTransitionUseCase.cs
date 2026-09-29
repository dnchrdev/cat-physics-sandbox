using Cysharp.Threading.Tasks;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.Shared.Utilities;
using ILogger = Features.Core.UseCases.Ports.ILogger;

namespace Features.Scene.UseCases
{
    public sealed class SceneTransitionUseCase : ISceneTransition
    {
        private readonly ISceneLoadingService _scenes;
        private readonly ILoadingScreenService _loadingScreen;
        private readonly ILogger _logger;

        public SceneTransitionUseCase(ISceneLoadingService scenes, ILoadingScreenService loadingScreen, ILogger logger)
        {
            _scenes = scenes;
            _loadingScreen = loadingScreen;
            _logger = logger;
        }

        public async UniTask<Result> SwitchAsync(SceneId from, SceneId to)
        {
            await _loadingScreen.FadeInAsync();

            var unload = await _scenes.UnloadAsync(from);
            if (!unload.IsSuccess)
                return Fail(unload);

            return await OpenCoveredAsync(to);
        }

        public async UniTask<Result> OpenCoveredAsync(SceneId scene)
        {
            var load = await _scenes.LoadAsync(scene, false);
            if (!load.IsSuccess)
                return Fail(load);

            await UniTask.WaitForEndOfFrame();

            var activate = await _scenes.ActivateAsync(scene);
            if (!activate.IsSuccess)
                return Fail(activate);

            await _loadingScreen.FadeOutAsync();
            return Result.Success();
        }

        private Result Fail(Result result)
        {
            _logger.LogError(GetType(), $"Scene transition failed: {result.Error}");
            return result;
        }
    }
}

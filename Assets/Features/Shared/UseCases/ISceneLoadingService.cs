using Cysharp.Threading.Tasks;
using Features.Shared.Entities;
using Features.Shared.Utilities;

namespace Features.Shared.UseCases
{
    public interface ISceneLoadingService
    {
        public UniTask<Result> LoadAsync(SceneId id, bool activateImmediately = true);
        public UniTask<Result> ActivateAsync(SceneId id);

        public UniTask<Result> UnloadAsync(SceneId id);
    }
}
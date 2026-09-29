using Cysharp.Threading.Tasks;
using Features.Shared.Entities;
using Features.Shared.Utilities;

namespace Features.Shared.UseCases
{
    public interface ISceneTransition
    {
        UniTask<Result> SwitchAsync(SceneId from, SceneId to);
        UniTask<Result> OpenCoveredAsync(SceneId scene);
    }
}

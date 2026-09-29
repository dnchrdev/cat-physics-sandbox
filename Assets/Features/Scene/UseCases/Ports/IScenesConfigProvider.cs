using Features.Scene.Entities;
using Features.Shared.Entities;
using Features.Shared.Utilities;

namespace Features.Scene.UseCases.Ports
{
    public interface IScenesConfigProvider
    {
        Result<SceneDescriptor> TryGetScene(SceneId id);
    }
}

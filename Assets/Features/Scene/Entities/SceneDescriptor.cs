using Features.Shared.Entities;

namespace Features.Scene.Entities
{
    public sealed class SceneDescriptor
    {
        public SceneId Id { get; }
        public SceneLoadMode LoadMode { get; }

        public SceneDescriptor(SceneId id, SceneLoadMode loadMode)
        {
            Id = id;
            LoadMode = loadMode;
        }
    }
}

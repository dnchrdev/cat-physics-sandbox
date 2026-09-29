using Features.Scene.Entities;
using Features.Scene.UseCases.Ports;

namespace Features.Scene.UseCases
{
    public class SceneEntry
    {
        public SceneState State { get; set; }
        public ISceneHandle Handle { get; }

        public SceneEntry(SceneState state, ISceneHandle handle)
        {
            State = state;
            Handle = handle;
        }
    }
}

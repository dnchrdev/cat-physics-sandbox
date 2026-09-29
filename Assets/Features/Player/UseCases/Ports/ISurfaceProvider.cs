using Features.Player.Entities;

namespace Features.Player.UseCases.Ports
{
    public interface ISurfaceProvider
    {
        SurfaceProperties DefaultSurface { get; }
        SurfaceProperties GetSurfaceUnderCharacter();
    }
}

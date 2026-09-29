using System.Linq;
using Features.Player.Entities;
using Features.Player.Infrastructure;
using Features.Player.Infrastructure.Configs;
using Features.Player.UseCases.Ports;

namespace Features.Player.InterfaceAdapters.Gateways
{
    public sealed class SurfaceGateway : ISurfaceProvider
    {
        private readonly CharacterMotor _motor;
        private readonly (string tag, SurfaceProperties surface)[] _taggedSurfaces;

        public SurfaceProperties DefaultSurface { get; }

        public SurfaceGateway(SurfacesConfig config, CharacterMotor motor)
        {
            _motor = motor;
            DefaultSurface = config.DefaultSurface.ToProperties();
            _taggedSurfaces = config.Surfaces
                .Select(data => (data.TagName, data.ToProperties()))
                .ToArray();
        }

        public SurfaceProperties GetSurfaceUnderCharacter()
        {
            var ground = _motor.GroundObject;
            if (ground == null) return DefaultSurface;

            foreach (var (tag, surface) in _taggedSurfaces)
                if (ground.CompareTag(tag))
                    return surface;

            return DefaultSurface;
        }
    }
}

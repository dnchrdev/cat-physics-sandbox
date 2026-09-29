using System.Collections.Generic;
using UnityEngine;

namespace Features.Player.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "SurfacesConfig", menuName = "Configs/Surfaces")]
    public class SurfacesConfig : ScriptableObject
    {
        [SerializeField] private SurfaceData _defaultSurface;
        [SerializeField] private SurfaceData[] _surfaces;

        public SurfaceData DefaultSurface => _defaultSurface;
        public IReadOnlyList<SurfaceData> Surfaces => _surfaces;
    }
}

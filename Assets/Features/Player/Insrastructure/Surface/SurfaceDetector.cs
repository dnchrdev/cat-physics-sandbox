using Features.Player.Insrastructure.Configs;
using UnityEngine;

namespace Features.Player.Insrastructure.Surface
{
    public class SurfaceDetector
    {
        private SurfacesConfig _surfaces;

        private Data.Surface currentSurface = null;

        public SurfaceDetector(SurfacesConfig surfaces)
        {
            _surfaces = surfaces;
        }

        public Data.Surface CurrentSurface => currentSurface;
        public Data.Surface DefaultSurface => _surfaces.DefaultSurface;

        public void ResetToDefault()
        {
            currentSurface = _surfaces.DefaultSurface;
        }

        public void ResetToUnstable()
        {
            currentSurface = _surfaces.UnstableSurface;
        }

        public void GetSurfaceData(GameObject ground)
        {
            if (ground != null)
            {
                bool validSurface = _surfaces.GetSurface(ground, out Data.Surface surface);

                if (validSurface)
                {
                    SetCurrentSurface(surface);
                }
                else
                {
                    SetCurrentSurface(_surfaces.DefaultSurface);
                }
            }
            else
            {
                SetCurrentSurface(_surfaces.DefaultSurface);
            }
        }

        private void SetCurrentSurface(Data.Surface surface)
        {
            currentSurface = surface;
        }
    }
}
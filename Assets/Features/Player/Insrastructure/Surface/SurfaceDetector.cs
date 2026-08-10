using UnityEngine;

namespace Feature.PlayerFeature
{
    public class SurfaceDetector
    {
        private SurfacesConfig _surfaces;

        private Surface currentSurface = null;

        public SurfaceDetector(SurfacesConfig surfaces)
        {
            _surfaces = surfaces;
        }

        public Surface CurrentSurface => currentSurface;
        public Surface DefaultSurface => _surfaces.DefaultSurface;

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
                bool validSurface = _surfaces.GetSurface(ground, out Surface surface);

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

        private void SetCurrentSurface(Surface surface)
        {
            currentSurface = surface;
        }
    }
}
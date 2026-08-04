using UnityEngine;

namespace Feature.PlayerFeature
{
    public class SurfaceDetector
    {
        private SurfacesConfigSO _surfacesConfig;

        private Surface currentSurface = null;

        public SurfaceDetector(SurfacesConfigSO surfacesConfig)
        {
            _surfacesConfig = surfacesConfig;
        }

        public Surface CurrentSurface => currentSurface;
        public Surface DefaultSurface => _surfacesConfig.DefaultSurface;

        public void ResetToDefault()
        {
            currentSurface = _surfacesConfig.DefaultSurface;
        }

        public void ResetToUnstable()
        {
            currentSurface = _surfacesConfig.UnstableSurface;
        }

        public void GetSurfaceData(GameObject ground)
        {
            if (ground != null)
            {
                bool validSurface = _surfacesConfig.GetSurface(ground, out Surface surface);

                if (validSurface)
                {
                    SetCurrentSurface(surface);
                }
                else
                {
                    SetCurrentSurface(_surfacesConfig.DefaultSurface);
                }
            }
            else
            {
                SetCurrentSurface(_surfacesConfig.DefaultSurface);
            }
        }

        private void SetCurrentSurface(Surface surface)
        {
            currentSurface = surface;
        }
    }
}
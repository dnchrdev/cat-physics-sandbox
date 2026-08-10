using UnityEngine;

namespace Feature.PlayerFeature
{
    [CreateAssetMenu(fileName = "SurfacesConfig", menuName = "Configs/Surfaces")]
    public class SurfacesConfig : ScriptableObject
    {
        [field: SerializeField] public Surface DefaultSurface { get; private set; }
        [field: SerializeField] public Surface UnstableSurface { get; private set; }

        [field: SerializeField] public Surface[] Surfaces { get; private set; }

        public bool GetSurface(GameObject gameObject, out Surface outputSurface)
        {
            outputSurface = null;

            for (int i = 0; i < Surfaces.Length; i++)
            {
                var surface = Surfaces[i];

                if (gameObject.CompareTag(surface.TagName))
                {
                    outputSurface = surface;
                    return true;
                }
            }

            return false;
        }
    }
}
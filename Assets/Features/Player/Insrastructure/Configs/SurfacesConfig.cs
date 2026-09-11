using UnityEngine;

namespace Features.Player.Insrastructure.Configs
{
    [CreateAssetMenu(fileName = "SurfacesConfig", menuName = "Configs/Surfaces")]
    public class SurfacesConfig : ScriptableObject
    {
        [field: SerializeField] public Data.Surface DefaultSurface { get; private set; }
        [field: SerializeField] public Data.Surface UnstableSurface { get; private set; }

        [field: SerializeField] public Data.Surface[] Surfaces { get; private set; }

        public bool GetSurface(GameObject gameObject, out Data.Surface outputSurface)
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
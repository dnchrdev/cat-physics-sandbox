using UnityEngine;

namespace Feature.PlayerFeature
{
    public class PlayerRig : MonoBehaviour
    {
        [field: SerializeField] public Transform UpperSphereCheckTransform { get; private set; }
        [field: SerializeField] public Transform LowerSphereCheckTransform { get; private set; }
        [field: SerializeField] public Transform GameStartTransform { get; private set; }
    }
}
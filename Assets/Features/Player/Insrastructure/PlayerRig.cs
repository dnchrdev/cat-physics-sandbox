using UnityEngine;

namespace Features.Player.Insrastructure
{
    public class PlayerRig : MonoBehaviour
    {
        [field: SerializeField] public Transform UpperSphereCheckTransform { get; private set; }
        [field: SerializeField] public Transform LowerSphereCheckTransform { get; private set; }
        [field: SerializeField] public Transform GameStartTransform { get; private set; }
    }
}
using UnityEngine;

namespace Features.Camera.Infrastructure
{
    public class CameraRefs : MonoBehaviour
    {
        [field: SerializeField] public Transform PositionRoot { get; private set; }
        [field: SerializeField] public Transform RotationRoot { get; private set; }
        [field: SerializeField] public Transform BobRoot { get; private set; }
        [field: SerializeField] public Transform SpringRoot { get; private set; }
        [field: SerializeField] public Transform LeanRoot { get; private set; }
    }
}

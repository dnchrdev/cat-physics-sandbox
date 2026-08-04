using System.Collections;
using UnityEngine;

namespace Feature.CameraFeature
{
    public class CameraRig: MonoBehaviour
    {
        [field: SerializeField] public Transform PositionRoot { get; private set; }
        [field: SerializeField] public Transform RotationRoot { get; private set; }
        [field: SerializeField] public Transform BobRoot { get; private set; }
        [field: SerializeField] public Transform SpringRoot { get; private set; }
        [field: SerializeField] public Transform LeanRoot { get; private set; }
    }
}
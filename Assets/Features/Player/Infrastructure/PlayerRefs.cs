using Features.Player.UseCases.Ports;
using UnityEngine;

namespace Features.Player.Infrastructure
{
    public class PlayerRefs : MonoBehaviour, ISpawnPoint
    {
        [field: SerializeField] public Transform GameStartTransform { get; private set; }

        public Vector3 Position => GameStartTransform.position;
        public Quaternion Rotation => GameStartTransform.rotation;
    }
}

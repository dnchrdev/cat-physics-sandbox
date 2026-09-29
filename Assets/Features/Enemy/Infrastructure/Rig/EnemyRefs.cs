using UnityEngine;

namespace Features.Enemy.Infrastructure.Rig
{
    public class EnemyRefs : MonoBehaviour
    {
        [field: SerializeField] public Transform Head { get; private set; }
    }
}

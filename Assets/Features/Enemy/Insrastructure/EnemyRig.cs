using UnityEngine;

namespace Features.Enemy.Insrastructure
{
    public class EnemyRig : MonoBehaviour
    {
        [field: SerializeField] public Transform Head { get; private set; }
    }
}
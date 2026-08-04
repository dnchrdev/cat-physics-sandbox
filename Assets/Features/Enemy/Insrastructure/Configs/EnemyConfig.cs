using UnityEngine;

namespace Feature.EnemyFeature
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "SO/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: Header("Common")]
        [field: SerializeField] public LayerMask GroundMask { get; internal set; }
        [field: SerializeField] public float WalkAnimationResponse { get; private set; } = 15f;

        [field: Header("Patroling")]
        [field: SerializeField] public float PatrolSpeed { get; private set; } = 1f;
        [field: SerializeField] public float AnimationPatrolSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float PatrolStoppingDistance { get; private set; } = 1f;
        [field: SerializeField] public float PatrolWaitMinDuration { get; private set; } = 1f;
        [field: SerializeField] public float PatrolWaitMaxDuration { get; private set; } = 2f;
        [field: SerializeField] public bool IsRandomPatroling { get; private set; } = false;
        [field: SerializeField] public bool IncludeReversedPatroling { get; internal set; } = false;
        [field: SerializeField] public int PatrolingIndexMinIncrement { get; internal set; } = 1;
        [field: SerializeField] public int PatrolingIndexMaxIncrement { get; internal set; } = 3;
        [field: SerializeField] public int PatrolingIndexMinReverseStrike { get; internal set; } = 1;
        [field: SerializeField] public int PatrolingIndexMaxReverseStrike { get; internal set; } = 3;

        [field: Header("Pursuit")]
        [field: SerializeField] public float HitMinVelocity { get; private set; } = 5f;
        [field: SerializeField] public float PlayerFindDistance { get; private set; } = 25f;
        [field: SerializeField] public float PursuitSpeed { get; private set; } = 2f;
        [field: SerializeField] public float AnimationPursuitSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float PursuitDuration { get; private set; } = 5f;
        [field: SerializeField] public float VisionSphereRadius { get; private set; } = 0.1f;

        [field: Header("Attaack")]
        [field: SerializeField] public float AttackStartMaxYawAngle { get; private set; } = 5f;
        [field: SerializeField] public float ApplyDamageMaxYawAngle { get; private set; } = 45f;
        [field: SerializeField] public float AttackRotationSpeed { get; private set; } = 350f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 1f;
        [field: SerializeField] public float AttackSphereDistance { get; private set; } = 1f;
        [field: SerializeField] public float AttackHorizontalDistance { get; private set; } = 1f;

        [field: Header("Ragdoll")]
        [field: SerializeField] public float RespawnDuration { get; private set; } = 5f;

        [field: Header("VisionAndLook")]
        [field: SerializeField] public LayerMask VisionMask { get; private set; }
        [field: SerializeField] public float HeadLookYawMaxAngle { get; private set; } = 45f;
        [field: SerializeField] public float HeadLookPitchMaxAngle { get; private set; } = 89f;
        [field: SerializeField] public float HeadLookResponse { get; private set; } = 10f;
        [field: SerializeField] public float PatrolingHeadLookMinSeconds { get; private set; } = 2f;
        [field: SerializeField] public float PatrolingHeadLookMaxSeconds { get; private set; } = 6f;
        [field: SerializeField] public float PatrolingHeadLookMinSecondsDelay { get; private set; } = 2f;
        [field: SerializeField] public float PatrolingHeadLookMaxSecondsDelay { get; private set; } = 6f;
    }
}
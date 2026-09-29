using Features.Enemy.Entities.Settings;
    using UnityEngine;

namespace Features.Enemy.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: Header("Common")]
        [SerializeField] private LayerMask _groundMask;
        public LayerMask GroundMask => _groundMask;

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
        [field: SerializeField] public float PursuitSpeed { get; private set; } = 2f;
        [field: SerializeField] public float AnimationPursuitSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float PursuitDuration { get; private set; } = 5f;

        [field: Header("Attack")]
        [field: SerializeField] public float AttackStartMaxYawAngle { get; private set; } = 5f;
        [field: SerializeField] public float ApplyDamageMaxYawAngle { get; private set; } = 45f;
        [field: SerializeField] public float AttackRotationSpeed { get; private set; } = 350f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 1f;
        [field: SerializeField] public float AttackSphereDistance { get; private set; } = 1f;
        [field: SerializeField] public float AttackHorizontalDistance { get; private set; } = 1f;

        [field: Header("Ragdoll")]
        [field: SerializeField] public float RespawnDuration { get; private set; } = 5f;

        [field: Header("VisionAndLook")]
        [SerializeField] private LayerMask _visionMask;
        public LayerMask VisionMask => _visionMask;

        [field: SerializeField] public float HeadLookYawMaxAngle { get; private set; } = 45f;
        [field: SerializeField] public float HeadLookPitchMaxAngle { get; private set; } = 89f;
        [field: SerializeField] public float HeadLookResponse { get; private set; } = 10f;
        [field: SerializeField] public float PatrolingHeadLookMinSeconds { get; private set; } = 2f;
        [field: SerializeField] public float PatrolingHeadLookMaxSeconds { get; private set; } = 6f;
        [field: SerializeField] public float PatrolingHeadLookMinSecondsDelay { get; private set; } = 2f;
        [field: SerializeField] public float PatrolingHeadLookMaxSecondsDelay { get; private set; } = 6f;

        public EnemySettings ToSettings() =>
            new(
                patrol: new PatrolSettings(
                    speed: PatrolSpeed,
                    stoppingDistance: PatrolStoppingDistance,
                    waitMinDuration: PatrolWaitMinDuration,
                    waitMaxDuration: PatrolWaitMaxDuration,
                    isRandom: IsRandomPatroling,
                    includeReversed: IncludeReversedPatroling,
                    minIncrement: PatrolingIndexMinIncrement,
                    maxIncrement: PatrolingIndexMaxIncrement,
                    minReverseStrike: PatrolingIndexMinReverseStrike,
                    maxReverseStrike: PatrolingIndexMaxReverseStrike,
                    glanceMinDuration: PatrolingHeadLookMinSeconds,
                    glanceMaxDuration: PatrolingHeadLookMaxSeconds,
                    glanceMinDelay: PatrolingHeadLookMinSecondsDelay,
                    glanceMaxDelay: PatrolingHeadLookMaxSecondsDelay),
                pursuit: new PursuitSettings(speed: PursuitSpeed),
                knockout: new KnockoutSettings(
                    minHitVelocity: HitMinVelocity,
                    restDuration: RespawnDuration,
                    angerDuration: PursuitDuration),
                attack: new AttackSettings(
                    startMaxYawAngle: AttackStartMaxYawAngle,
                    damageMaxYawAngle: ApplyDamageMaxYawAngle,
                    rotationSpeed: AttackRotationSpeed,
                    cooldown: AttackCooldown,
                    sphereDistance: AttackSphereDistance,
                    horizontalDistance: AttackHorizontalDistance),
                headLook: new HeadLookSettings(
                    yawMaxAngle: HeadLookYawMaxAngle,
                    pitchMaxAngle: HeadLookPitchMaxAngle,
                    response: HeadLookResponse),
                walkAnimation: new WalkAnimationSettings(
                    patrolSpeed: PatrolSpeed,
                    pursuitSpeed: PursuitSpeed,
                    patrolAnimationSpeed: AnimationPatrolSpeed,
                    pursuitAnimationSpeed: AnimationPursuitSpeed,
                    response: WalkAnimationResponse));
    }
}

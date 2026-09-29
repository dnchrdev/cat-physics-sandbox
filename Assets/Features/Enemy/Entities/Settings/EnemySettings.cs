namespace Features.Enemy.Entities.Settings
{
    public sealed class EnemySettings
    {
        public PatrolSettings Patrol { get; }
        public PursuitSettings Pursuit { get; }
        public KnockoutSettings Knockout { get; }
        public AttackSettings Attack { get; }
        public HeadLookSettings HeadLook { get; }
        public WalkAnimationSettings WalkAnimation { get; }

        public EnemySettings(
            PatrolSettings patrol,
            PursuitSettings pursuit,
            KnockoutSettings knockout,
            AttackSettings attack,
            HeadLookSettings headLook,
            WalkAnimationSettings walkAnimation)
        {
            Patrol = patrol;
            Pursuit = pursuit;
            Knockout = knockout;
            Attack = attack;
            HeadLook = headLook;
            WalkAnimation = walkAnimation;
        }
    }
}

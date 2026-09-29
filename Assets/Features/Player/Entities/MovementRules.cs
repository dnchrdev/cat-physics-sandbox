namespace Features.Player.Entities
{
    public sealed class MovementRules
    {
        public float JumpSpeed { get; }
        public float CoyoteTime { get; }
        public float SlideFriction { get; }
        public float SlideGravity { get; }
        public float Gravity { get; }

        public MovementRules(float jumpSpeed, float coyoteTime, float slideFriction, float slideGravity, float gravity)
        {
            JumpSpeed = jumpSpeed;
            CoyoteTime = coyoteTime;
            SlideFriction = slideFriction;
            SlideGravity = slideGravity;
            Gravity = gravity;
        }
    }
}

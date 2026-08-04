using UnityEngine;

namespace Feature.PlayerFeature
{
    public class AliveCharacterState : BaseCharacterState
    {
        private bool _prevJump;

        public AliveCharacterState(MovementContext context, MovementState state) : base(context, state)
        {
        }

        public override void Enter()
        {
            base.Enter();

            MoveInput.HorizontalMoveEvent += UpdateHorizontalMove;

            MoveInput.JumpStartEvent += JumpStarted;
            MoveInput.JumpReleaseEvent += JumpReleased;
        }

        public override void Exit()
        {
            base.Exit();

            MoveInput.HorizontalMoveEvent -= UpdateHorizontalMove;

            MoveInput.JumpStartEvent -= JumpStarted;
            MoveInput.JumpReleaseEvent -= JumpReleased;
        }

        public override void FixedTick(float dt)
        {
            base.FixedTick(dt);
            State.LastState = State.State;
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);

            if (Player.IsAlive == false)
            {
                StateSwitcher.Switch<DeadCharacterState>();
                return;
            }

            UpdateImputs(dt);

            State.TimeSinceJumped += dt;
        }

        private void UpdateImputs(float dt)
        {
            var currentJump = State.RequestedJump;
            State.RequestedJump = State.RequestedJump && !_prevJump;
            _prevJump = currentJump;

            var wasRequestedJump = State.RequestedJump;
            State.RequestedJump = State.RequestedJump || (State.RequestedSustainJump);

            if (State.RequestedJump && !wasRequestedJump && currentJump)
            {
                State.TimeSinceJumpRequest = 0f;
            }
        }

        private void UpdateHorizontalMove(Vector2 moveInput)
        {
            if (CameraRig == null || CameraRig.RotationRoot == null) return;

            State.RequestedRotation = CameraRig.RotationRoot.transform.rotation;
            State.RequestedMovement = new Vector3(moveInput.x, 0f, moveInput.y);
            State.RequestedMovement = State.RequestedRotation * State.RequestedMovement;
            State.RequestedMovement = Vector3.ProjectOnPlane(State.RequestedMovement, Vector3.up).normalized *
                                      State.RequestedMovement.magnitude;
        }

        private void JumpStarted()
        {
            State.RequestedJump = true;
            State.RequestedSustainJump = true;
        }

        private void JumpReleased()
        {
            State.RequestedJump = false;
            State.RequestedSustainJump = false;
        }
    }
}
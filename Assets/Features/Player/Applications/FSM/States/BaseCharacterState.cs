using Features.Camera.Infrastructure;
using Features.Input.Infrastructure.Interfaces;
using Features.Player.Domain;
using Features.Player.Insrastructure.Configs;
using Features.Player.Insrastructure.Interfaces;
using Features.Player.Insrastructure.Interfaces.Context;
using Features.Player.Insrastructure.Surface;
using UnityEngine;

namespace Features.Player.Applications.FSM.States
{
    public class BaseCharacterState : ICharacterState
    {
        protected MovementState State;
        private MovementContext _ctx;

        protected CameraRig CameraRig => _ctx.CameraRig;
        protected IStateSwitcher StateSwitcher => _ctx.ModuleSwitcher;
        protected IMovementInput MoveInput => _ctx.MoveInput;
        protected IInteractionInput InteractInput => _ctx.InteractInput;
        protected Domain.Player Player => _ctx.Player;
        protected ICharacterMotor Motor => _ctx.Motor;
        protected Vector3 MotorPosition => _ctx.ReadOnlyMotor.GetPosition();
        protected PlayerCharacterConfig Config => _ctx.Config;
        protected Vector3 CameraForward => _ctx.ReadOnlyCamera.Forward;
        protected SurfaceDetector SurfaceDetector => _ctx.SurfaceDetector;
        protected SlidePhysicsCalculator UnstableSlideCalculator => _ctx.SlidePhysicsCalculator;


        protected bool Stop;

        public BaseCharacterState(
            MovementContext context,
            MovementState state
        )
        {
            _ctx = context;
            State = state;
        }

        public virtual void Enter()
        {
            Stop = false;
            State.CharacterState = this;
        }

        public virtual void Exit()
        {
            Stop = false;
        }

        public virtual void FixedTick(float dt)
        {
            Stop = false;
        }

        public virtual void Tick(float dt)
        {
            Stop = false;
        }
    }
}
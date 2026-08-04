using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class CharacterStateMachine : ITickable, IFixedTickable, IStateSwitcher
    {
        private ICharacterState _current;
        private ICharacterState _defaultState;
        private List<ICharacterState> _states = new();

        public ICharacterState LastState { get; private set; }

        [Inject]
        public void Construct(
            MovementState state,
            MovementContext movementContext
        )
        {
            _defaultState = new WalkCharacterState(movementContext, state);

            _states.Clear();

            _states.Add(_defaultState);
            _states.Add(new AirborneCharacterState(movementContext, state));
            _states.Add(new JumpCharacterState(movementContext, state));
            _states.Add(new DeadCharacterState(movementContext, state));

            Switch<WalkCharacterState>();
        }

        public void Tick()
        {
            _current?.Tick(Time.deltaTime);
        }

        public void FixedTick()
        {
            _current?.FixedTick(Time.fixedDeltaTime);
        }

        public void Switch<T>() where T : ICharacterState
        {
            if (_current is T)
                return;

            LastState = _current;

            var next = _states.FirstOrDefault(m => m is T);

            if (next == null) throw new Exception("Forgotted State");

            if (ReferenceEquals(_current, next))
                return;

            _current?.Exit();

            _current = next;

            _current.Enter();
        }
    }
}
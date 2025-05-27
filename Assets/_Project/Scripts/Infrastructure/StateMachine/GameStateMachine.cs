using System;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, BaseGameState> _states = new();
        private BaseGameState _currentState;
        public event Action<Type> OnStateChanged;

        public void AddState(BaseGameState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void Enter<TState>() where TState : BaseGameState
        {
            var type = typeof(TState);
            if (_currentState?.GetType() == type) return;
            if (!_states.TryGetValue(type, out var state)) return;

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            OnStateChanged?.Invoke(type);
        }

        public void Tick()
        {
            _currentState?.Update();
        }

        public void Dispose()
        {
            _currentState?.Exit();
            _states.Clear();
        }
    }
}
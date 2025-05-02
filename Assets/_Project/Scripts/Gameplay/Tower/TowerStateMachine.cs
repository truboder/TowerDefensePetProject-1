using System;
using System.Collections.Generic;
using Gameplay.Tower.States;

namespace Gameplay.Tower
{
    public class TowerStateMachine : IDisposable
    {
        private readonly Dictionary<Type, BaseTowerState> _states = new();
        public BaseTowerState CurrentState { get; private set; }
        public event Action<Type> OnStateChanged;

        public void AddState(BaseTowerState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : BaseTowerState
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
            {
                return;
            }

            if (!_states.TryGetValue(type, out var state))
            {
                return;
            }

            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
            
            OnStateChanged?.Invoke(type);
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void Dispose()
        {
            CurrentState?.Exit();
            _states.Clear();
        }
    }
}
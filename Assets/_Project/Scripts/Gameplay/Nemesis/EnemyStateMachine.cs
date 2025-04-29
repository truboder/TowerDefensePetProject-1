using System;
using System.Collections.Generic;
using Gameplay.Nemesis.States;

namespace Gameplay.Nemesis
{
    public class EnemyStateMachine : IDisposable
    {
        private readonly Dictionary<Type, BaseEnemyState> _states = new();
        public BaseEnemyState CurrentState { get; private set; }
        public event Action<Type> OnStateChanged;

        public void AddState(BaseEnemyState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : BaseEnemyState
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
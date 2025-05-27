using System;
using Zenject;

namespace Infrastructure.StateMachine
{
    public interface IGameStateMachine : ITickable
    {
        void AddState(BaseGameState state);
        void Enter<TState>() where TState : BaseGameState;
        void Dispose();
        event Action<Type> OnStateChanged;
    }
}
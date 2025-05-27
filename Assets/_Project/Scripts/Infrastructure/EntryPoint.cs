using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure
{
    public class EntryPoint : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        [Inject]
        public EntryPoint(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}
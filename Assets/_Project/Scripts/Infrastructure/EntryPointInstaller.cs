using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEntryPointServices();
        }

        private void BindEntryPointServices()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EntryPoint>().AsSingle().NonLazy();

            var gameStateMachine = Container.Resolve<IGameStateMachine>();
            gameStateMachine.AddState(new BootstrapState(gameStateMachine));
            gameStateMachine.AddState(new GameplayState(gameStateMachine));
        }
    }
}
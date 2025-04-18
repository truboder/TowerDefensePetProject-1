using _Project.Scripts.Utils;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunService>().To<CoroutineRunService>().AsSingle().NonLazy();
        }
    }
}
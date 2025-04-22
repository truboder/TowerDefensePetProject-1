using Utils;
using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunService>().To<CoroutineRunService>().AsSingle().NonLazy();
        }
    }
}
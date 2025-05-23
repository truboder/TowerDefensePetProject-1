using Common.Coroutines;
using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CoroutineRunService>().AsSingle().NonLazy();
        }
    }
}
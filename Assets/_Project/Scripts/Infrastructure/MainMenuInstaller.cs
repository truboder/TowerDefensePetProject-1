using Gameplay.MainMenu.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuUI _mainMenuUI;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuUI>().FromInstance(_mainMenuUI).AsSingle().NonLazy();
        }
    }
}
using UnityEngine;
using Zenject;

namespace Features.GameBootstrap.Installer
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrap _gameBootstrap;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameBootstrap>().FromInstance(_gameBootstrap).AsSingle();
        }
    }
}
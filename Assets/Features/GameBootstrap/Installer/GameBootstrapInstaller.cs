using Zenject;
using UnityEngine;

namespace Feature.GameBootstrap
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstap _gameBootstrap;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameBootstap>().FromInstance(_gameBootstrap).AsSingle();
        }
    }
}
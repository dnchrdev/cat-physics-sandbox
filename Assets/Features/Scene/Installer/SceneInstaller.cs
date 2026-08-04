using System;
using Zenject;
using UnityEngine;

namespace Feature.Scene
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ScenesConfig _scenesConfig;
        public override void InstallBindings()
        {
            Container.Bind<SceneLoaderService>().AsSingle();
            Container.Bind<ScenesConfig>().FromInstance(_scenesConfig).AsSingle();
            Container.Bind<SceneManager>().AsSingle();
        }
    }
}

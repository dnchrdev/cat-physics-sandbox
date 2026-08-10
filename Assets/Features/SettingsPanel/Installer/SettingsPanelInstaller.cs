
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.SettingsPanel
{
    public class SettingsPanelInstaller: MonoInstaller
    {
        [SerializeField] private SettingsView _view;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SettingsPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SettingsView>().FromInstance(_view).AsSingle();
        }
    }
}
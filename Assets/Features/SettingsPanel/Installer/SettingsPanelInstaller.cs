using Features.SettingsPanel.Adapter;
using Features.SettingsPanel.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.SettingsPanel.Installer
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
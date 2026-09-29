using Features.SettingsPanel.Infrastructure;
using Features.SettingsPanel.InterfaceAdapters.Controllers;
using Features.SettingsPanel.InterfaceAdapters.Presenters;
using Features.SettingsPanel.UseCases;
using UnityEngine;
using Zenject;

namespace Features.SettingsPanel.Installer
{
    public sealed class SettingsPanelInstaller : MonoInstaller
    {
        [SerializeField] private SettingsView _view;

        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesTo<SettingsView>().FromInstance(_view).AsSingle();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<SettingsController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SettingsPresenter>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<SettingsInteractor>().AsSingle();
        }
    }
}

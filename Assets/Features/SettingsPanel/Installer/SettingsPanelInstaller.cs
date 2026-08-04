
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.SettingsPanel
{
    public class SettingsPanelInstaller: MonoInstaller
    {
        [SerializeField] private SettingsPresenter _presenter;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SettingsPresenter>().FromInstance(_presenter).AsSingle();
        }
    }
}
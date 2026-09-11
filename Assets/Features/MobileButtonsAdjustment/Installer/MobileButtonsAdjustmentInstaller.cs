using Features.MobileButtonsAdjustment.Adapter;
using Features.MobileButtonsAdjustment.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.MobileButtonsAdjustment.Installer
{
    public class MobileButtonsAdjustmentInstaller : MonoInstaller
    {
        [SerializeField] private MobileButtonsAdjustmentView _mobileButtonsAdjustmentView;
        
        public override void InstallBindings()
        {
            Container.Bind<MobileAdjustmentEventBus>().AsSingle();
            Container.BindInterfacesAndSelfTo<MobileButtonsAdjustmentPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MobileButtonsAdjustmentView>().FromInstance(_mobileButtonsAdjustmentView).AsSingle();
        }
    }
}
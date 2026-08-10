using Feature.Storage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.MobileButtonsAdjustment
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
using Features.MobileButtonsAdjustment.Entities;
using Features.MobileButtonsAdjustment.Infrastructure.Views;
using Features.MobileButtonsAdjustment.InterfaceAdapters.Controllers;
using Features.MobileButtonsAdjustment.InterfaceAdapters.Presenters;
using Features.MobileButtonsAdjustment.UseCases;
using UnityEngine;
using Zenject;

namespace Features.MobileButtonsAdjustment.Installer
{
    public class MobileButtonsAdjustmentInstaller : MonoInstaller
    {
        [SerializeField] private MobileButtonsAdjustmentView _mobileButtonsAdjustmentView;

        public override void InstallBindings()
        {
            Container.Bind<ButtonLayoutEditSession>().AsSingle();

            Container.Bind<OpenButtonsAdjustmentUseCase>().AsSingle();
            Container.Bind<MoveButtonUseCase>().AsSingle();
            Container.Bind<ResetButtonPositionsUseCase>().AsSingle();
            Container.Bind<ClosePanelUseCase>().AsSingle();

            Container.BindInterfacesTo<MobileButtonsAdjustmentController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MobileButtonsAdjustmentPresenter>().AsSingle();

            Container.BindInterfacesTo<MobileButtonsAdjustmentView>()
                .FromInstance(_mobileButtonsAdjustmentView).AsSingle();
        }
    }
}

using Feature.Storage;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.MobileButtonsAdjustment
{
    public class MobileButtonsAdjustmentInstaller : MonoInstaller
    {
        [SerializeField] private MobileAdjustmentPresenter _mobileAdjustmentPanel;

        private IReadOnlyControlSettings _controlSettings;

        [Inject]
        private void Construct(IReadOnlyControlSettings controlSettings)
        {
            _controlSettings = controlSettings;
        }

        public override void InstallBindings()
        {
            if(_controlSettings.IsMobile)
                Container.BindInterfacesAndSelfTo<MobileAdjustmentPresenter>().FromInstance(_mobileAdjustmentPanel).AsSingle().NonLazy();
            else
                _mobileAdjustmentPanel.gameObject.SetActive(false);
        }
    }
}
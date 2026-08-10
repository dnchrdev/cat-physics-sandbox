using Feature.Storage;
using UnityEngine;
using Zenject;

namespace Feature.Quests
{
    public class QuestsInstaller : MonoInstaller
    {
        [SerializeField] private QuestsConfig _config;
        [SerializeField] private QuestsBootstrap _bootstrap;
        [SerializeField] private PCCurrentQuestView _pcCurrentQuestView;
        [SerializeField] private MobileCurrentQuestView _mobileCurrentQuestView;
        [SerializeField] private AllQuestsView _allQuestsView;
        [SerializeField] private QuestHintsView _questHintsView;

        private IReadOnlyControlSettings _controlSettings;

        [Inject]
        private void Construct(IReadOnlyControlSettings controlSettings)
        {
            _controlSettings = controlSettings;
        }

        public override void InstallBindings()
        {
            Container.Bind<HintShowUseCase>().AsSingle();
            Container.Bind<ResetAllQuestsUseCase>().AsSingle();
            Container.Bind<QuestCardFactory>().AsSingle();
            Container.Bind<QuestCaldsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<QuestTipsManager>().AsSingle();
            Container.Bind<HintsFactory>().AsSingle();

            Container.Bind<QuestsConfig>().FromInstance(_config).AsSingle();
            Container.BindInterfacesTo<QuestsBootstrap>().FromInstance(_bootstrap).AsSingle();
            Container.Bind<QuestsCollection>().AsSingle();

            if (_controlSettings.IsMobile)
            {
                Container.BindInterfacesTo<MobileCurrentQuestView>().FromInstance(_mobileCurrentQuestView).AsSingle();
                Container.BindInterfacesTo<CurrentQuestPresenter>().AsSingle().NonLazy();;
                _pcCurrentQuestView.gameObject.SetActive(false);
            }
            else
            {
                Container.BindInterfacesTo<PCCurrentQuestView>().FromInstance(_pcCurrentQuestView).AsSingle();
                Container.BindInterfacesTo<CurrentQuestPresenter>().AsSingle().NonLazy();
                _mobileCurrentQuestView.gameObject.SetActive(false);
            }

            Container.BindInterfacesAndSelfTo<AllQuestsPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<QuestHintsPresenter>().AsSingle().NonLazy();

            Container.BindInterfacesTo<AllQuestsView>().FromInstance(_allQuestsView).AsSingle();
            Container.BindInterfacesTo<QuestHintsView>().FromInstance(_questHintsView).AsSingle();
            
        }
    }
}

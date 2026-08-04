using Feature.Storage;
using UnityEngine;
using Zenject;

namespace Feature.Quests
{
    public class QuestsInstaller : MonoInstaller
    {
        [SerializeField] private QuestsConfig _config;
        [SerializeField] private QuestsBootstrap _bootstrap;
        [SerializeField] private CurrentQuestView _pcCurrentQuestView;
        [SerializeField] private CurrentQuestView _mobileCurrentQuestView;
        [SerializeField] private AllQuestsView _allQuestsView;
        [SerializeField] private QuestHitsView _questHitsView;

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
            Container.Bind<TipFactory>().AsSingle();

            Container.Bind<QuestsConfig>().FromInstance(_config).AsSingle();
            Container.BindInterfacesTo<QuestsBootstrap>().FromInstance(_bootstrap).AsSingle();
            Container.Bind<QuestsCollection>().AsSingle();

            if (_controlSettings.IsMobile)
            {
                Container.Bind<CurrentQuestView>().FromInstance(_mobileCurrentQuestView).AsSingle();
                Container.BindInterfacesTo<CurrentQuestPresenter>().AsSingle().NonLazy();;
                _pcCurrentQuestView.gameObject.SetActive(false);
            }
            else
            {
                Container.Bind<CurrentQuestView>().FromInstance(_pcCurrentQuestView).AsSingle();
                Container.BindInterfacesTo<CurrentQuestPresenter>().AsSingle().NonLazy();
                _mobileCurrentQuestView.gameObject.SetActive(false);
            }

            Container.Bind<AllQuestsView>().FromInstance(_allQuestsView).AsSingle();
            Container.Bind<AllQuestsPresenter>().AsSingle().NonLazy();

            Container.Bind<QuestHitsView>().FromInstance(_questHitsView).AsSingle();
            Container.BindInterfacesAndSelfTo<QuestHintsPresenter>().AsSingle().NonLazy();
            
        }
    }
}

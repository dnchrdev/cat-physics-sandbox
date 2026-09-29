using Features.Quests.Entities;
using Features.Quests.Infrastructure;
using Features.Quests.Infrastructure.Config;
using Features.Quests.Infrastructure.Factories;
using Features.Quests.Infrastructure.Managers;
using Features.Quests.Infrastructure.Repositories;
using Features.Quests.Infrastructure.Views;
using Features.Quests.InterfaceAdapters.Controllers;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Quests.InterfaceAdapters.Presenters;
using Features.Quests.UseCases;
using UnityEngine;
using Zenject;

namespace Features.Quests.Installer
{
    public class QuestsInstaller : MonoInstaller
    {
        [SerializeField] private QuestsConfig _config;
        [SerializeField] private QuestsRefs _refs;
        [SerializeField] private PCCurrentQuestView _pcView;
        [SerializeField] private MobileCurrentQuestView _mobileView;
        [SerializeField] private AllQuestsView _allQuestsView;
        [SerializeField] private QuestHintsView _questHintsView;

        public override void InstallBindings()
        {
            InstallEntities();
            InstallUseCases();
            InstallInterfaceAdapters();
            InstallInfrastructure();
        }

        private void InstallEntities()
        {
            Container.Bind<QuestsCollection>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<CurrentQuestReporter>().AsSingle();
            Container.BindInterfacesTo<StartQuestsUseCase>().AsSingle();
            Container.Bind<RecordQuestProgressUseCase>().AsSingle();
            Container.Bind<RequestQuestHintsUseCase>().AsSingle();
            Container.Bind<SelectQuestUseCase>().AsSingle();
            Container.Bind<ResetAllQuestsUseCase>().AsSingle();
            Container.Bind<QuestListUseCase>().AsSingle();
            Container.Bind<QuestsNavigationUseCase>().AsSingle();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<QuestProgressController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<QuestsUIController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CurrentQuestPresenter>().AsSingle();
            Container.BindInterfacesTo<QuestHintsPresenter>().AsSingle();
            Container.BindInterfacesTo<QuestListPresenter>().AsSingle();
        }

        private void InstallInfrastructure()
        {
            Container.Bind<QuestsConfig>().FromInstance(_config).AsSingle();
            Container.Bind<QuestsRefs>().FromInstance(_refs).AsSingle();
            Container.BindInterfacesTo<QuestsRepository>().AsSingle();

            foreach (var quest in _refs.GetQuests())
                Container.Bind<IQuestSource>().FromInstance(quest);

            Container.Bind<QuestCardFactory>().AsSingle();
            Container.Bind<HintObjectFactory>().AsSingle();
            Container.BindInterfacesTo<QuestCardsManager>().AsSingle();
            Container.BindInterfacesTo<QuestHintsManager>().AsSingle();

            Container.BindInterfacesTo<PCCurrentQuestView>().FromInstance(_pcView).AsSingle();
            Container.BindInterfacesTo<MobileCurrentQuestView>().FromInstance(_mobileView).AsSingle();
            Container.BindInterfacesAndSelfTo<AllQuestsView>().FromInstance(_allQuestsView).AsSingle();
            Container.BindInterfacesAndSelfTo<QuestHintsView>().FromInstance(_questHintsView).AsSingle();
        }
    }
}

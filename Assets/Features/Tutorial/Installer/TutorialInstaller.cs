using Features.Tutorial.Infrastructure;
using Features.Tutorial.InterfaceAdapters;
using Features.Tutorial.UseCases;
using UnityEngine;
using Zenject;

namespace Features.Tutorial.Installer
{
    public sealed class TutorialInstaller : MonoInstaller
    {
        [SerializeField] private TutorialRefs _tutorialRefs;
        [SerializeField] private TutorialCompletedView _tutorialCompletedView;

        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesTo<TutorialRefs>().FromInstance(_tutorialRefs).AsSingle();
            Container.BindInterfacesTo<TutorialCompletedView>().FromInstance(_tutorialCompletedView).AsSingle().NonLazy();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<TutorialController>().AsSingle().NonLazy();
        }

        private void InstallUseCases()
        {
            Container.Bind<TutorialCompletedChecker>().AsSingle();
            Container.Bind<StartGameplayUseCase>().AsSingle();
        }
    }
}

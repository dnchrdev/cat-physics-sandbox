using Features.Tutorial.Adapter;
using Features.Tutorial.Application;
using Features.Tutorial.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.Tutorial.Installer
{
    public class TutorialInstaller : MonoInstaller
    {
        [SerializeField] private TutorialCompletedChecker _tutorialCompletedChecker;
        [SerializeField] private TutorialCompletedView _tutorialCompletedView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameStartedUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TutorialCompletedChecker>().FromInstance(_tutorialCompletedChecker).AsSingle();
            Container.Bind<CompleteTutorialUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialCompletedPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TutorialCompletedView>().FromInstance(_tutorialCompletedView).AsSingle().NonLazy();
        }
    }
}

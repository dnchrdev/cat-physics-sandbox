using Feature.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialInstaller : MonoInstaller
{
    [SerializeField] private TutorialCompletedChecker _tutorialCompletedChecker;
    [SerializeField] private TutorialCompletedView _tutorialCompletedView;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TutorialCompletedChecker>().FromInstance(_tutorialCompletedChecker).AsSingle();
        Container.Bind<CompleteTutorialUseCase>().AsSingle();
        Container.Bind<TutorialCompletedPresenter>().AsSingle().NonLazy();
        Container.BindInterfacesTo<TutorialCompletedView>().FromInstance(_tutorialCompletedView).AsSingle().NonLazy();
    }
}

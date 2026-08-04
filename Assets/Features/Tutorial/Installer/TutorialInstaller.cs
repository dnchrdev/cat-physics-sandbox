using Feature.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialInstaller : MonoInstaller
{
    [SerializeField] private TutorialCompletedChecker _tutorialCompletedChecker;
    [SerializeField] private TutorialPresenter _presenter;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TutorialCompletedChecker>().FromInstance(_tutorialCompletedChecker).AsSingle();
        Container.Bind<CompleteTutorialUseCase>().AsSingle();
        Container.BindInterfacesTo<TutorialPresenter>().FromInstance(_presenter).AsSingle().NonLazy();
    }
}

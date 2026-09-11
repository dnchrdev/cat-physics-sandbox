using Features.Enemy.Application;
using Features.Enemy.Domain;
using Features.Enemy.Insrastructure;
using Features.Enemy.Insrastructure.Configs;
using Features.Enemy.Insrastructure.Ragdoll;
using Features.Enemy.Presentation.Animation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Features.Enemy.Installer
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyRig _enemyRig;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyConfig _enemyConfig;
        //[SerializeField] private PatrolPoints _patrolPoints;
        [SerializeField] private RagdollController _ragdollController;
        [SerializeField] private EnemyAnimationEvents _enemyAnimationEvents;

        public override void InstallBindings()
        {
            //Domain
            Container.BindInterfacesAndSelfTo<Domain.Enemy>().AsSingle();
            Container.Bind<Blackboard>().AsSingle();
            
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();

            Container.Bind<EnemyRig>().FromInstance(_enemyRig).AsSingle();

            Container.Bind<RagdollController>().FromInstance(_ragdollController).AsSingle();

            Container.Bind<IAnimatableEnemy>().To<AnimatableEnemy>().AsSingle();
            Container.Bind<IMovableEnemy>().To<MovableEnemy>().AsSingle();
            Container.Bind<EnemyVisionAndLook>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAnimationSpeedCalculator>().AsSingle();
            Container.Bind<EnemyUseCaseOrchestrator>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAttackAbility>().AsSingle();
            Container.Bind<EnemyPursuitUseCase>().AsSingle();
            Container.Bind<EnemyKnockoutedUseCase>().AsSingle();

            Container.Bind<EnemyContext>().AsSingle();

            Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle();
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle();

            Container.BindInterfacesTo<EnemyController>().AsSingle().NonLazy();

            Container.Bind<EnemyAnimationEvents>().FromInstance(_enemyAnimationEvents).AsSingle();
        }
    }
}
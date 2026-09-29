using Features.Enemy.Entities;
using Features.Enemy.Entities.Settings;
using Features.Enemy.Infrastructure.Configs;
using Features.Enemy.Infrastructure.Ragdoll;
using Features.Enemy.Infrastructure.Rig;
using Features.Enemy.InterfaceAdapters.BT;
using Features.Enemy.InterfaceAdapters.Controllers;
using Features.Enemy.InterfaceAdapters.Gateways;
using Features.Enemy.InterfaceAdapters.Presenters;
using Features.Enemy.UseCases;
using Features.Enemy.UseCases.Tasks;
using Features.Enemy.UseCases.Tasks.Patrolling;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Features.Enemy.Installer
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyRefs enemyRefs;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private RagdollController _ragdollController;
        [SerializeField] private EnemyAnimationEvents _enemyAnimationEvents;

        public override void InstallBindings()
        {
            InstallEntities();
            InstallUseCases();
            InstallInterfaceAdapters();
            InstallInfrastructure();
        }

        private void InstallEntities()
        {
            Container.BindInterfacesAndSelfTo<Entities.Enemy>().AsSingle();
            Container.Bind<PatrolRoute>().AsSingle();
            Container.Bind<GlanceSchedule>().AsSingle();
            Container.Bind<HeadLook>().AsSingle();
            Container.Bind<EnemyAttack>().AsSingle();

            var settings = _enemyConfig.ToSettings();
            Container.Bind<PatrolSettings>().FromInstance(settings.Patrol).AsSingle();
            Container.Bind<PursuitSettings>().FromInstance(settings.Pursuit).AsSingle();
            Container.Bind<KnockoutSettings>().FromInstance(settings.Knockout).AsSingle();
            Container.Bind<AttackSettings>().FromInstance(settings.Attack).AsSingle();
            Container.Bind<HeadLookSettings>().FromInstance(settings.HeadLook).AsSingle();
            Container.Bind<WalkAnimationSettings>().FromInstance(settings.WalkAnimation).AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<WalkAnimationUseCase>().AsSingle();
            Container.Bind<EnemyAttackUseCase>().AsSingle();
            Container.Bind<EnemyLifecycleUseCase>().AsSingle();
            Container.Bind<EnemyHeadLookUseCase>().AsSingle();
            Container.Bind<EnemyUpdateUseCase>().AsSingle();

            Container.Bind<TaskRagdolling>().AsSingle();
            Container.Bind<TaskAttacking>().AsSingle();
            Container.Bind<TaskPursuitPlayer>().AsSingle();
            Container.Bind<TaskPatrolGlancing>().AsSingle();
            Container.Bind<TaskFindGroundedPatrolPoint>().AsSingle();
            Container.Bind<TaskPatrolWaiting>().AsSingle();
            Container.Bind<TaskPatrolingToPoint>().AsSingle();
            Container.Bind<TaskFindNewPatrolingIndex>().AsSingle();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<EnemyController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyBehaviourTree>().AsSingle();
            Container.BindInterfacesTo<EnemyAnimationPresenter>().AsSingle();

            Container.BindInterfacesTo<EnemyMoverGateway>().AsSingle();
            Container.BindInterfacesTo<EnemyHeadGateway>().AsSingle();
            Container.BindInterfacesTo<EnemyVisionGateway>().AsSingle();
            Container.BindInterfacesTo<GroundRaycastGateway>().AsSingle();
            Container.BindInterfacesTo<UnityRandomGateway>().AsSingle();
        }

        private void InstallInfrastructure()
        {
            Container.Bind<EnemyRefs>().FromInstance(enemyRefs).AsSingle();
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle();

            Container.BindInterfacesTo<RagdollController>().FromInstance(_ragdollController).AsSingle();
            Container.BindInterfacesTo<EnemyAnimationEvents>().FromInstance(_enemyAnimationEvents).AsSingle();
            Container.BindInterfacesTo<EnemyAnimatorIK>().FromComponentOn(_animator.gameObject).AsSingle();
        }
    }
}

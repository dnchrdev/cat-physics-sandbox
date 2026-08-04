using System;
using System.Collections.Generic;
using Feature.BehaviourTree;
using Feature.Core;
using Feature.PlayerFeature;
using UnityEngine;
using Zenject;
using Tree = Feature.BehaviourTree.Tree;

namespace Feature.EnemyFeature
{
    public class EnemyController : IInitializable, IDisposable, ITickable
    {
        [Inject] private readonly IGamePauseService _pauseService;
        [Inject] private readonly EnemyContext _ctx;
        [Inject] private readonly Player _player;
        [Inject] private readonly EnemyAnimationEvents _animationEvents;
        [Inject] private readonly EnemyUseCaseOrchestrator _useCaseOrchestrator;
        [Inject] private readonly EnemyAttackAbility _attackAbility;
        [Inject] private readonly Blackboard _bb;
        [Inject] private readonly EnemyVisionAndLook _visionAndLook;

        private Tree _enemyBT;

        public void Initialize()
        {
            var ragdoll = new Sequence(
                new List<Node>
                {
                    new TaskRagdolling(_ctx, _bb)
                });

            var attack = new Sequence(
                new List<Node>
                {
                    new TaskAttacking(_ctx, _bb)
                });

            var pursuit = new Sequence(
                new List<Node>
                {
                    new TaskPursuitPlayer(_ctx, _bb)
                }
            );

            var patrol = new Sequence(
                new List<Node>
                {
                    new TaskFindGroundedPatrolPoint(_ctx, _bb),
                    new TaskPatrolWaiting(_ctx, _bb),
                    new TaskPatrolingToPoint(_ctx, _bb),
                    new TaskFindNewPatrolingIndex(_ctx, _bb)
                });


            Node root = new Sequence(
                new List<Node>
                {
                    new TaskUpdateTicks(_ctx, _bb),
                    new Selector(
                        new List<Node>
                        {
                            ragdoll,
                            attack,
                            pursuit,
                            patrol
                        })
                });

            _enemyBT = new Tree();
            _enemyBT.Initialize(root);


            _ctx.Enemy.HitRecieved += _useCaseOrchestrator.HandleOnHitRecieved;
            _ctx.Enemy.Continiued += _useCaseOrchestrator.HandleOnEnemyRespawned;

            _player.Respawned += _useCaseOrchestrator.OnPlayerRespawned;
            _player.Continiued += _useCaseOrchestrator.OnPlayerRespawned;
            
            _animationEvents.AttackStartedEvent += _attackAbility.Start;
            _animationEvents.ApplyDamageEvent += _attackAbility.ApplyDamage;
            _animationEvents.AttackFinishedEvent += _attackAbility.Finish;
        }

        public void Dispose()
        {
            _ctx.Enemy.HitRecieved -= _useCaseOrchestrator.HandleOnHitRecieved;
            _ctx.Enemy.Continiued -= _useCaseOrchestrator.HandleOnEnemyRespawned;

            _player.Respawned -= _useCaseOrchestrator.OnPlayerRespawned;
            _player.Continiued -= _useCaseOrchestrator.OnPlayerRespawned;
            
            _animationEvents.AttackStartedEvent -= _attackAbility.Start;
            _animationEvents.ApplyDamageEvent -= _attackAbility.ApplyDamage;
            _animationEvents.AttackFinishedEvent -= _attackAbility.Finish;
        }

        public void Tick()
        {
            if (_pauseService.Paused == false)
                _enemyBT.Tick(Time.deltaTime);
            
            _visionAndLook.Tick(Time.deltaTime);
        }
    }
}
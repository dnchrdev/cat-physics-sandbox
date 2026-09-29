using System.Collections.Generic;
using Features.BehaviourTree.Entities;
using Features.BehaviourTree.Entities.Composite;
using Features.Enemy.UseCases.Ports;
using Features.Enemy.UseCases.Tasks;
using Features.Enemy.UseCases.Tasks.Patrolling;
using Tree = Features.BehaviourTree.Entities.Tree;

namespace Features.Enemy.InterfaceAdapters.BT
{
    public sealed class EnemyBehaviourTree : IEnemyBehaviour
    {
        private readonly Tree _tree = new();

        public EnemyBehaviourTree(
            TaskRagdolling ragdolling,
            TaskAttacking attacking,
            TaskPursuitPlayer pursuit,
            TaskPatrolGlancing patrolGlancing,
            TaskFindGroundedPatrolPoint findGroundedPatrolPoint,
            TaskPatrolWaiting patrolWaiting,
            TaskPatrolingToPoint patrolingToPoint,
            TaskFindNewPatrolingIndex findNewPatrolingIndex)
        {
            var patrol = new Sequence(new List<Node>
            {
                Leaf(patrolGlancing),
                Leaf(findGroundedPatrolPoint),
                Leaf(patrolWaiting),
                Leaf(patrolingToPoint),
                Leaf(findNewPatrolingIndex)
            });

            var root = new Selector(new List<Node>
            {
                Leaf(ragdolling),
                Leaf(attacking),
                Leaf(pursuit),
                patrol
            });

            _tree.Initialize(root);
        }

        public void Evaluate(float dt) => _tree.Tick(dt);

        private static Node Leaf(IEnemyTask task) => new BehaviourTaskNode(task);
    }
}

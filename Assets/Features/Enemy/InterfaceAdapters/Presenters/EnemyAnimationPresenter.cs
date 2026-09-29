using Features.Enemy.InterfaceAdapters.Ports;
using Features.Enemy.UseCases.Ports;
using UnityEngine;

namespace Features.Enemy.InterfaceAdapters.Presenters
{
    public sealed class EnemyAnimationPresenter : IEnemyAnimationOutput
    {
        private static readonly int WalkSpeed = Animator.StringToHash("WalkSpeed");
        private static readonly int Pursuit = Animator.StringToHash("Pursuit");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int AttackStop = Animator.StringToHash("AttackStop");
        private const string IdleWalkState = "Idle/Walk";

        private readonly Animator _animator;
        private readonly IHeadLookView _headLookView;

        public EnemyAnimationPresenter(Animator animator, IHeadLookView headLookView)
        {
            _animator = animator;
            _headLookView = headLookView;
        }

        public void SetAnimated(bool isAnimated) => _animator.enabled = isAnimated;

        public void SetWalkSpeed(float animationSpeed) => _animator.SetFloat(WalkSpeed, animationSpeed);

        public void SetPursuit(bool isPursuing) => _animator.SetBool(Pursuit, isPursuing);

        public void PlayAttack()
        {
            _animator.ResetTrigger(AttackStop);
            _animator.SetTrigger(Attack);
        }

        public void CancelAttack()
        {
            _animator.SetTrigger(AttackStop);
            _animator.ResetTrigger(Attack);
            _animator.Play(IdleWalkState, 0, 0f);
        }

        public void SetHeadLook(Vector3 target, float weight) => _headLookView.SetLookAt(target, weight);
    }
}

using UnityEngine;
using Zenject;

namespace Feature.EnemyFeature
{
    public class AnimatableEnemy: IAnimatableEnemy
    {
        [Inject] public readonly EnemyConfig Config;
        [Inject] public readonly EnemyAnimationSpeedCalculator AnimatorSpeedCalculator;
        [Inject] private readonly Animator _animator;

        private bool _isPursuing;
        
        public void Enable()
        {
            _animator.enabled = true;
        }

        public void Disable()
        {
            _animator.enabled = false;
        }
        
        public void UpdateWalkSpeed(float dt)
        {
            var maxSpeed = _isPursuing ? Config.PursuitSpeed : Config.PatrolSpeed;
            var maxAnimationSpeed = _isPursuing ? Config.AnimationPursuitSpeed : Config.AnimationPatrolSpeed;
            
            var walkSpeed = AnimatorSpeedCalculator.GetWalkAnimationSpeed(maxSpeed, maxAnimationSpeed, dt);
            _animator.SetFloat("WalkSpeed", walkSpeed);
        }

        public void SetPursuit(bool isPursuing)
        { 
            _isPursuing = isPursuing;
            _animator.SetBool("Pursuit", isPursuing);
        }

        public void AttackStart()
        {
            _animator.SetTrigger("Attack");
        }
        
        public void ResetAttack()
        {
            _animator.ResetTrigger("Attack");
        }
        
        public void AttackStop()
        {
            _animator.SetTrigger("AttackStop");
        }
        
        public void ResetSttackStop()
        {
            _animator.ResetTrigger("AttackStop");
        }

        public void PlayIdleWalk()
        {
            _animator.Play("Idle/Walk", 0, 0f);
        }
        
    }
}
using UnityEngine;

namespace Features.Enemy.UseCases.Ports
{
    public interface IEnemyAnimationOutput
    {
        void SetAnimated(bool isAnimated);

        void SetWalkSpeed(float animationSpeed);
        void SetPursuit(bool isPursuing);
        void PlayAttack();
        void CancelAttack();
        void SetHeadLook(Vector3 target, float weight);
    }
}

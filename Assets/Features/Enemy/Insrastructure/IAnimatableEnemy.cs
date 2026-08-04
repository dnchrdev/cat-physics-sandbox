namespace Feature.EnemyFeature
{
    public interface IAnimatableEnemy
    {
        void UpdateWalkSpeed(float dt);
        void SetPursuit(bool isPursuing);
        public void AttackStart();

        public void ResetAttack();

        public void AttackStop();

        public void ResetSttackStop();

        public void PlayIdleWalk();

        void Enable();
        void Disable();
    }
}
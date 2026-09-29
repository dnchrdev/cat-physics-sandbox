namespace Features.Enemy.Entities
{
    public sealed class EnemyAttack
    {
        private float _cooldownLeft = -1f;

        public bool IsPlaying { get; private set; }
        public bool IsReady => !IsPlaying && _cooldownLeft <= 0f;

        public void Tick(float dt) => _cooldownLeft -= dt;

        public bool TryBegin(float cooldown)
        {
            if (!IsReady)
                return false;

            _cooldownLeft = cooldown;
            return true;
        }

        public void MarkPlaying() => IsPlaying = true;
        public void Stop() => IsPlaying = false;
    }
}

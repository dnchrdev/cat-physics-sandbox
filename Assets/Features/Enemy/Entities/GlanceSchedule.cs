namespace Features.Enemy.Entities
{
    public sealed class GlanceSchedule
    {
        private float _phaseTimeLeft;

        public bool IsGlancing { get; private set; }

        public bool Tick(float dt)
        {
            _phaseTimeLeft -= dt;
            if (_phaseTimeLeft > 0f)
                return false;

            IsGlancing = !IsGlancing;
            return true;
        }

        public void SetPhaseDuration(float seconds) => _phaseTimeLeft = seconds;
    }
}

namespace Features.MobileButtonsAdjustment.Entities
{
    public sealed class ButtonLayoutEditSession
    {
        public bool HasChanges { get; private set; }

        public void Begin() => HasChanges = false;
        public void MarkChanged() => HasChanges = true;
        public void End() => HasChanges = false;
    }
}

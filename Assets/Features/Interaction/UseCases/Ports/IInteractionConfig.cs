namespace Features.Interaction.UseCases.Ports
{
    public interface IInteractionConfig
    {
        float MinDistanceBetweenPlayerAndInteractableMesh { get; }
        float ReleaseCooldown { get; }
        float ForceGrabbedReleaseDistance { get; }
        float GrabbedThrowPower { get; }
        float FocusHitPower { get; }
    }
}

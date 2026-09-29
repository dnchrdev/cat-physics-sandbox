namespace Features.Interaction.Entities
{
    public enum InteractionType
    {
        None,
        Full,
        Hit,
        Grab
    }

    public enum GrabType
    {
        None,
        CenterWithRotation,
        Center,
        NearestPoint
    }

    public enum InteractableState
    {
        Idle,
        Focused,
        Grabbed
    }
}

namespace Features.Interaction.UseCases
{
    public readonly struct InteractionRequest
    {
        public readonly bool Grab;
        public readonly bool Hit;
        public readonly bool Throw;
        public readonly bool Release;

        public InteractionRequest(bool grab, bool hit, bool @throw, bool release)
        {
            Grab = grab;
            Hit = hit;
            Throw = @throw;
            Release = release;
        }
    }
}

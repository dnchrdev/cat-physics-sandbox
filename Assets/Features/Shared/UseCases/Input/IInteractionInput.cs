namespace Features.Shared.UseCases.Input
{
    public interface IInteractionInput
    {
        bool IsHit { get; }
        bool IsGrab { get; }
        bool IsThrow { get; }
        bool IsRelease { get; }
        void InvokeHit();
        void InvokeGrab();
        void InvokeThrow();
        void InvokeRelease();
    }
}
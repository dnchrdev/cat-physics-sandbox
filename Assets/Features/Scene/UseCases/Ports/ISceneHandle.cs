namespace Features.Scene.UseCases.Ports
{
    public interface ISceneHandle
    {
        bool IsValid { get; }
        bool Succeeded { get; }
    }
}

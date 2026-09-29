using Features.Shared.Utilities;

namespace Features.Storage.UseCases.Ports
{
    public interface IPlayerStorage
    {
        Result IsAvailable();
        SaveData Read();
        void Write(SaveData data);
        void Commit();
    }
}

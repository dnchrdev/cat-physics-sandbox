using Cysharp.Threading.Tasks;

namespace Features.Shared.UseCases
{
    public interface IStorageDataService
    {
        UniTask Load();
        void Save();
    }
}

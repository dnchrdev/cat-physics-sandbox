using Cysharp.Threading.Tasks;

namespace Features.Shared.UseCases
{
    public interface ILoadingScreenService
    {
        UniTask FadeInAsync();
        UniTask FadeOutAsync();
    }
}

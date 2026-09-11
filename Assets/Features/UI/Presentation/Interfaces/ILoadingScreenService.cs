using Cysharp.Threading.Tasks;

namespace Features.UI.Presentation.Interfaces
{
    public interface ILoadingScreenService
    {
        UniTask FadeInAsync();
        UniTask FadeOutAsync();
    }
}
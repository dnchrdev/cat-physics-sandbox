using Cysharp.Threading.Tasks;
using Feature.Core;
using Feature.Scene;

namespace Feature.MainMenu
{
    public interface IStartGameUseCase
    {
        UniTask StartGameplayAsync();
    }
}
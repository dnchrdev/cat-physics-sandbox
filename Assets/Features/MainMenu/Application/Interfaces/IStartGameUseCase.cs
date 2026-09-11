using Cysharp.Threading.Tasks;

namespace Features.MainMenu.Application.Interfaces
{
    public interface IStartGameUseCase
    {
        UniTask StartGameplayAsync();
    }
}
namespace Features.MainMenu.UseCases.Ports
{
    public interface IMainMenuOutputPort
    {
        void ShowStartGameAvailable(bool isAvailable);
        void ShowControlChoice(bool isVisible);
    }
}

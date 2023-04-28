using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Interfaces;

public interface IBoardPage
{
    Task SetStartGameMode(GameMode value);
}


public interface INavigationService
{
    Task NavigateToPage<T>() where T : ContentPage;

    Task NavigateToBoardPage<T>(GameModeModel mode) where T : ContentPage, IBoardPage;

    Task NavigateBack();

    INavigation Navigation { get; }
}
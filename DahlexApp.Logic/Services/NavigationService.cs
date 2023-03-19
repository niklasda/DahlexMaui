using DahlexApp.Logic.Models;
using System.Diagnostics;

namespace DahlexApp.Logic.Services;

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

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _services;

    public INavigation Navigation
    {
        get
        {
            INavigation navigation = Application.Current!.MainPage!.Navigation;
            {
                return navigation;
            }
            //if (navigation is not null)
            //else
            //{
            //    //This is not good!
            //    if (Debugger.IsAttached)
            //        Debugger.Break();
            //    throw new Exception();
            //}
        }
    }

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }

    public Task NavigateToPage<T>() where T : ContentPage
    {
        var page = ResolvePage<T>();
        if (page is not null)
        {
            return Navigation.PushAsync(page, true);
        }

        throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
    }

    public async Task NavigateToBoardPage<T>(GameModeModel mode) where T : ContentPage, IBoardPage
    {
        var page = ResolvePage<T>();
        if (page is not null)
        {
            await Navigation.PushAsync(page, true);
            //    await Task.Delay(1000);
            // page.StartGameMode = mode.SelectedGameMode;
            await page.SetStartGameMode(mode.SelectedGameMode);
        }
    }

    public Task NavigateBack()
    {
        if (Navigation.NavigationStack.Count > 1)
        {
            return Navigation.PopAsync();
        }

        throw new InvalidOperationException("No pages to navigate back to!");
    }

    private T? ResolvePage<T>() where T : Page => _services.GetService<T>();
}
using System.Diagnostics;

namespace DahlexApp.Logic.Services;

public interface INavigationService
{
    Task NavigateToPage<T>() where T : Page;
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
            INavigation? navigation = Application.Current?.MainPage?.Navigation;
            if (navigation is not null)
                return navigation;
            else
            {
                //This is not good!
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw new Exception();
            }
        }
    }

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }

    //public Task NavigateToHowPage()        => NavigateToPage<HowPage>();

    public Task NavigateToPage<T>() where T : Page
    {
        var page = ResolvePage<T>();
        if (page is not null)
        {
            return Navigation.PushAsync(page, true);
        }

        throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
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
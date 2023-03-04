using CommunityToolkit.Mvvm.DependencyInjection;

namespace DahlexApp;

public partial class App
{
    public App(IServiceProvider services)
    {
        InitializeComponent();

        Ioc.Default.ConfigureServices(services);

        MainPage = new AppShell();
    }
}
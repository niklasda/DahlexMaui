using CommunityToolkit.Mvvm.DependencyInjection;

namespace DahlexApp;

public partial class App : Application
{
	public App(IServiceProvider services)
	{
		InitializeComponent();

        Ioc.Default.ConfigureServices(services);

        MainPage = new AppShell();
	}
}

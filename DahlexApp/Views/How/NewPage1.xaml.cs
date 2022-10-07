using CommunityToolkit.Mvvm.DependencyInjection;
using DahlexApp.Views.How;

namespace DahlexApp.Views;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();

        var vm = Ioc.Default.GetRequiredService<HowViewModel>();

        BindingContext = vm;
        //    NavigationPage.SetHasNavigationBar(this, false);
    }
}
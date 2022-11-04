using CommunityToolkit.Mvvm.DependencyInjection;
using DahlexApp.Views.How;

namespace DahlexApp.Views;

public partial class HowPage : ContentPage
{
	public HowPage()
	{
		InitializeComponent();

        var vm = Ioc.Default.GetRequiredService<HowViewModel>();

        BindingContext = vm;
        //    NavigationPage.SetHasNavigationBar(this, false);
    }
}
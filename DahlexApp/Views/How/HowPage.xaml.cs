using CommunityToolkit.Mvvm.DependencyInjection;
using DahlexApp.Views.How;

namespace DahlexApp.Views;

public partial class HowPage 
{
	public HowPage(HowViewModel vm)
	{
        BindingContext = vm;
		InitializeComponent();

        //var vm = Ioc.Default.GetRequiredService<HowViewModel>();

        //    NavigationPage.SetHasNavigationBar(this, false);
    }
}
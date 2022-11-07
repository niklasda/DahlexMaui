
namespace DahlexApp.Views.How;

public partial class HowPage 
{
	public HowPage(HowViewModel vm)
	{
        BindingContext = vm;
		InitializeComponent();
        //NavigationPage.SetHasNavigationBar(this, false);

    }
}
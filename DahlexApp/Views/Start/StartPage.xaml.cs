
namespace DahlexApp.Views.Start;


public partial class MainPage 
{

	public MainPage(StartViewModel vm)
    {
        BindingContext = vm;
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);

    }

}


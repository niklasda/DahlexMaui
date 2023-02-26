namespace DahlexApp.Views.Start;

public partial class StartPage 
{

	public StartPage(StartViewModel vm)
    {
        BindingContext = vm;
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
    }
}


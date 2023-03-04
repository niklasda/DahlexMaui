namespace DahlexApp.Views.Settings;

public partial class SettingsPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is SettingsViewModel vm)
        {
            vm.OnDisappearing();
        }
    }
}
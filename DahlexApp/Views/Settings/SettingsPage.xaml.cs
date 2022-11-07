
using CommunityToolkit.Mvvm.DependencyInjection;
using DahlexApp.Views.How;

namespace DahlexApp.Views.Settings
{
    public partial class SettingsPage //: MvxContentPage<SettingsViewModel>
    {
        public SettingsPage(SettingsViewModel vm)
        {
            BindingContext =vm;
            InitializeComponent();
            //  NavigationPage.SetHasNavigationBar(this, false);
            //6var vm = Ioc.Default.GetRequiredService<SettingsViewModel>();
        }
    }
}

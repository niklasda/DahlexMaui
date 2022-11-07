
namespace DahlexApp.Views.Settings;

    public partial class SettingsPage 
    {
        public SettingsPage(SettingsViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }

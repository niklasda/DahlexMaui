

namespace DahlexApp.Views.Scores;

    public partial class ScoresPage 
    {
        public ScoresPage(ScoresViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            //NavigationPage.SetHasNavigationBar(this, false);

        }
    }

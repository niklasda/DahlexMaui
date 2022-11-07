

namespace DahlexApp.Views.Scores;

    public partial class ScoresPage 
    {
        public ScoresPage(ScoresViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);

        }
    }

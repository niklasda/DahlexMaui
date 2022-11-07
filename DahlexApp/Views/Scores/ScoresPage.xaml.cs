
using CommunityToolkit.Mvvm.DependencyInjection;
using DahlexApp.Views.How;

namespace DahlexApp.Views.Scores
{
    public partial class ScoresPage //: MvxContentPage<ScoresViewModel>
    {
        public ScoresPage(ScoresViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);

        }
    }
}

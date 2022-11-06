//using Xamarin.Forms;

//using AndroidX.Lifecycle;

namespace DahlexApp.Views.Board
{
    public partial class BoardPage //: MvxContentPage<BoardViewModel>
    {
        public BoardPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);

           
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is BoardViewModel vm)
            {
                vm.OnAppearing().GetAwaiter().GetResult();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is BoardViewModel vm)
            {
                  vm.OnDisappearing();
            }
        }


        //protected override void OnViewModelSet()
        //{
        //    base.OnViewModelSet();

        //    ViewModel.TheAbsBoard = TheBoard;
        //    ViewModel.TheAbsOverBoard = TheOverBoard;
        //}
    }
}

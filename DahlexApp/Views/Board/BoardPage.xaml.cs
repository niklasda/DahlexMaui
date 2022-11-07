//using Xamarin.Forms;

//using AndroidX.Lifecycle;

using CommunityToolkit.Mvvm.DependencyInjection;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Services;

namespace DahlexApp.Views.Board
{
    public partial class BoardPage : IBoardPage //: MvxContentPage<BoardViewModel>
    {
        public BoardPage(BoardViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);

           

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           
        }

        

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            // if (BindingContext is BoardViewModel vm)
            //{
            //    vm.OnAppearing().GetAwaiter().GetResult();
            //}
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
        public GameModeModel StartGameMode
        {
            
            set
            {
                if (BindingContext is BoardViewModel vm)
                {
                    vm.StartGameMode = value;
                    vm.TheAbsBoard = TheBoard;
                    vm.TheAbsOverBoard = TheOverBoard;

                    vm.OnAppearing().GetAwaiter().GetResult();
                }
            }
        }
    }
}

using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Services;
using DahlexApp.Views.Board;
using DahlexApp.Views.How;
using DahlexApp.Views.Scores;
using DahlexApp.Views.Settings;
//using MvvmCross.Commands;
//using MvvmCross.Navigation;
//using MvvmCross.ViewModels;
//using Xamarin.Forms;

namespace DahlexApp.Views.Start
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel(INavigationService navigationService)
        {
           // _navigationService = navigationService;

            Title = "Dahlex";

            LogoImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.Tile300.png"); // 42x42

            HowCommand = new AsyncRelayCommand(async () => await navigationService.NavigateToPage<HowPage>()
                
            //Application.Current.MainPage.DisplayAlert("Dahlex","Coming SoOon","Ok");
            );

            //GotoBoardCommand = new RelayCommand(() =>

            //    _ = Task.Run(async () => await navigationService.NavigateToPage<BoardPage>(new GameModeModel { SelectedGameMode = GameMode.Random }))
            //);

            //GotoTutorialCommand = new Command(() =>

            //    _ = Task.Run(async () => await navigationService.NavigateToPage<BoardPageCampaign>(new GameModeModel { SelectedGameMode = GameMode.Campaign }))
            //);

            //GotoSettingsCommand = new Command(() =>

            //    _ = Task.Run(async () => await navigationService.NavigateToPage<SettingsPage>())
            //);

            //GotoScoresCommand = new Command(() =>

            //    _ = Task.Run(async () => await navigationService.NavigateToPage<ScoresPage>())
            //);
        }

        //private readonly IMvxNavigationService _navigationService;

        public ImageSource LogoImageSource { get; set; }

        // todo add base model with navigation etc


        //public override void Prepare()
        //{
        //    // first callback. Initialize parameter-agnostic stuff here
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();

        //    // do the heavy work here
        //}

        public IAsyncRelayCommand HowCommand { get; }
        public IAsyncRelayCommand GotoBoardCommand { get; }
        public IAsyncRelayCommand GotoTutorialCommand { get; }
        public IAsyncRelayCommand GotoSettingsCommand { get; }
        public IAsyncRelayCommand GotoScoresCommand { get; }



        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}

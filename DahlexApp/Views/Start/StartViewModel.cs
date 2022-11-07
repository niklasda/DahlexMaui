using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Services;
using DahlexApp.Views.Board;
using DahlexApp.Views.How;
using DahlexApp.Views.Scores;
using DahlexApp.Views.Settings;

namespace DahlexApp.Views.Start;

    public class StartViewModel : ObservableObject
    {

        public StartViewModel(INavigationService navigationService)
        {
            _title = string.Empty;
            Title = "Dahlex";

            LogoImageSource = ImageSource.FromFile("tile300.png"); // 42x42

            HowCommand = new AsyncRelayCommand(async () =>
                await navigationService.NavigateToPage<HowPage>());

            GotoBoardCommand = new AsyncRelayCommand(async () => await navigationService.NavigateToBoardPage<BoardPage>(new GameModeModel { SelectedGameMode = GameMode.Random }));
            

            GotoTutorialCommand = new AsyncRelayCommand(async () => await navigationService.NavigateToBoardPage<BoardPage>(new GameModeModel { SelectedGameMode = GameMode.Campaign }));

            GotoSettingsCommand = new AsyncRelayCommand(async () => await navigationService.NavigateToPage<SettingsPage>());

            GotoScoresCommand = new AsyncRelayCommand(async () => await navigationService.NavigateToPage<ScoresPage>());
        }
    

        public ImageSource LogoImageSource { get; set; }

        public IAsyncRelayCommand HowCommand { get; }
        public IAsyncRelayCommand GotoBoardCommand { get; }
        public IAsyncRelayCommand GotoTutorialCommand { get; }
        public IAsyncRelayCommand GotoSettingsCommand { get; }
        public IAsyncRelayCommand GotoScoresCommand { get; }



        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }

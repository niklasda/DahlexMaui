//using System.Linq;
//using System.Threading.Tasks;
//using DahlexApp.Logic.Settings;
//using MvvmCross.Base;
//using MvvmCross.Commands;
//using MvvmCross.Navigation;
//using MvvmCross.ViewModels;
//using Xamarin.Forms;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Services;
using DahlexApp.Logic.Settings;

namespace DahlexApp.Views.Scores;

    public class ScoresViewModel : ObservableObject
    {

        public ScoresViewModel(IHighScoreService scores, INavigationService navigationService)
        {
            _scores = scores;
            // _dispatcher = dispatcher;
            //_navigationService = navigationService;

            BackCommand = new AsyncRelayCommand( navigationService.NavigateBack);
            CloseImage = ImageSource.FromFile("close.png");

            Title = "Scores";

            HighScoreList.Clear();

            var scoreList = _scores.LoadLocalHighScores();
            var scoreItems = scoreList.Select(_ => new ScoreItemViewModel { Title = _.Content });

            foreach (var scoreItemViewModel in scoreItems)
            {
                HighScoreList.Add(scoreItemViewModel);

        }
    }

        private readonly IHighScoreService _scores;

      //  private readonly IMvxMainThreadAsyncDispatcher _dispatcher;
        // private readonly IMvxNavigationService _navigationService;

        //public override void Prepare()
        //{
        //    // first callback. Initialize parameter-agnostic stuff here
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();

        //    // do the heavy work here
        //}

        public IAsyncRelayCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }


        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ObservableCollection<ScoreItemViewModel> HighScoreList { get; } = new ObservableCollection<ScoreItemViewModel>();

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();

        //    _ = _dispatcher.ExecuteOnMainThreadAsync(() =>
        //      {
        //          Title = "Scores";

        //          HighScoreList.Clear();

        //          var scores = _scores.LoadLocalHighScores();
        //          HighScoreList.AddRange(scores.Select(_ => new ScoreItemViewModel { Title = _.Content }));
        //      });
        //}
    }

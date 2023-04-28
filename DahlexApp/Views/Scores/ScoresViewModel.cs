using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Settings;
using System.Collections.ObjectModel;
using DahlexApp.Logic.Interfaces;
using JetBrains.Annotations;

namespace DahlexApp.Views.Scores;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
public partial class ScoresViewModel : ObservableObject
{
    public ScoresViewModel(IHighScoreService scores, INavigationService navigationService)
    {
        //_scores = scores;

        BackCommand = new AsyncRelayCommand(navigationService.NavigateBack);
        CloseImage = ImageSource.FromFile("close.png");

        Title = "Scores";

        HighScoreList.Clear();

        var scoreList = scores.LoadLocalHighScores();
        var scoreItems = scoreList.Select(s => new ScoreItemViewModel(s.Content));

        foreach (var scoreItemViewModel in scoreItems)
        {
            HighScoreList.Add(scoreItemViewModel);
        }
    }

    //private readonly IHighScoreService _scores;

    public IAsyncRelayCommand BackCommand { get; set; }

    public ImageSource CloseImage { get; set; }

    [ObservableProperty]
    private string _title;

    //public string Title
    //{
    //    get => _title;
    //    [MemberNotNull(nameof(_title))]
    //    set => SetProperty(ref _title, value);
    //}

    public ObservableCollection<ScoreItemViewModel> HighScoreList { get; } = new ObservableCollection<ScoreItemViewModel>();
}
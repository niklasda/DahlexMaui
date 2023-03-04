using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Services;
using DahlexApp.Logic.Settings;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace DahlexApp.Views.Scores;

public class ScoresViewModel : ObservableObject
{
    public ScoresViewModel(IHighScoreService scores, INavigationService navigationService)
    {
        _scores = scores;

        BackCommand = new AsyncRelayCommand(navigationService.NavigateBack);
        CloseImage = ImageSource.FromFile("close.png");

        Title = "Scores";

        HighScoreList.Clear();

        var scoreList = _scores.LoadLocalHighScores();
        var scoreItems = scoreList.Select(_ => new ScoreItemViewModel(_.Content));

        foreach (var scoreItemViewModel in scoreItems)
        {
            HighScoreList.Add(scoreItemViewModel);
        }
    }

    private readonly IHighScoreService _scores;

    public IAsyncRelayCommand BackCommand { get; set; }

    public ImageSource CloseImage { get; set; }

    private string _title;

    public string Title
    {
        get => _title;
        [MemberNotNull(nameof(_title))]
        set => SetProperty(ref _title, value);
    }

    public ObservableCollection<ScoreItemViewModel> HighScoreList { get; } = new ObservableCollection<ScoreItemViewModel>();
}
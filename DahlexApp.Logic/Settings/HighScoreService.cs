using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using System.Text.Json;
using JetBrains.Annotations;

namespace DahlexApp.Logic.Settings;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
public class HighScoreService : IHighScoreService
{
    public HighScoreService(IPreferencesService preferences)
    {
        _preferences = preferences;
        _scores = new List<HighScore>();
        _scores = LoadLocalHighScores();
    }

    private const string Key = "HighScores";

    private readonly IPreferencesService _preferences;
    private List<HighScore> _scores;//= new List<HighScore>();

    public void AddHighScore(GameMode mode, string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, IntSize boardSize)
    {
        if (mode == GameMode.Random)
        {
            var hs = new HighScore(name, level, bombsLeft, teleportsLeft, moves, startTime, boardSize);
            _scores.Add(hs);
        }
    }

    public List<HighScore> LoadLocalHighScores()
    {
        try
        {
            var hsList = _preferences.LoadPreference(Key);
            //var settings = ApplicationData.Current.LocalSettings;

            if (!string.IsNullOrEmpty(hsList))
            {
                _scores = JsonSerializer.Deserialize<List<HighScore>>(hsList) ?? new List<HighScore>();
                //_scores = JsonConvert.DeserializeObject<List<HighScore>>(hsList);
            }

            if (_scores.Count == 0)
            {
                _scores.Add(new HighScore("Niklas (Beat me, I wanna be last)", 1, 1, 1, 1, DateTime.Now.AddMinutes(-3), new IntSize(12, 12)));
            }

            _scores.Sort(new HighScoreComparer());
            _scores = _scores.GetRange(0, Math.Min(_scores.Count, 20));
        }
        catch
        {
            //_scores.Clear();
        }

        return _scores;
    }

    public void SaveLocalHighScores()
    {
        string output = JsonSerializer.Serialize(_scores);
        _preferences.SavePreference(Key, output);
    }

    private class HighScoreComparer : IComparer<HighScore>
    {
        public int Compare(HighScore? x, HighScore? y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null && y != null)
            {
                return 1;
            }
            if (x != null && y == null)
            {
                return -1;
            }
            int cmp = y!.Score.CompareTo(x!.Score);
            if (cmp == 0)
            {
                return x.GameDuration.CompareTo(y.GameDuration);
            }
            else
            {
                return cmp;
            }
        }
    }
}
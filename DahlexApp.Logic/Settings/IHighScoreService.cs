using Size = System.Drawing.Size;
using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Settings
{
    // todo why was score 0
    public interface IHighScoreService
    {
        void AddHighScore(GameMode mode, string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, Size boardSize);
        List<HighScore> LoadLocalHighScores();
        void SaveLocalHighScores();
    }

}

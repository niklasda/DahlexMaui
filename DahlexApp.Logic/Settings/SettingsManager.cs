using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Settings;

public class SettingsManager : ISettingsManager
{
    private readonly IntSize _canvasSize;

    public SettingsManager(IntSize canvasSize)
    {
        _canvasSize = canvasSize;
    }


    public GameSettings LoadLocalSettings()
    {
        var settings = new GameSettings(_canvasSize);

        IPreferencesService prf = new PreferencesService();
        string playerName = prf.LoadPreference(Key1);
        if (string.IsNullOrEmpty(playerName))
        {
            settings.PlayerName = "Dr. Who";
        }
        else
        {
            settings.PlayerName = playerName;
        }

        string lessSound = prf.LoadPreference(Key2);

        bool.TryParse(lessSound, out bool less);

        settings.LessSound = less;

        return settings;
    }

    private const string Key1 = "SettingsName";
    private const string Key2 = "SettingsMute";

    public void SaveLocalSettings(GameSettings settings)
    {
        IPreferencesService prf = new PreferencesService();
        prf.SavePreference(Key1, settings.PlayerName);
        prf.SavePreference(Key2, settings.LessSound.ToString());
    }
}

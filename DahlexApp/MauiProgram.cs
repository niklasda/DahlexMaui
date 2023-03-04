using CommunityToolkit.Maui;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Services;
using DahlexApp.Logic.Settings;
using DahlexApp.Views.Board;
using DahlexApp.Views.How;
using DahlexApp.Views.Scores;
using DahlexApp.Views.Settings;
using DahlexApp.Views.Start;
using Plugin.Maui.Audio;

namespace DahlexApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<StartPage, StartViewModel>();
        builder.Services.AddTransient<HowPage, HowViewModel>();
        builder.Services.AddTransient<SettingsPage, SettingsViewModel>();
        builder.Services.AddTransient<ScoresPage, ScoresViewModel>();
        builder.Services.AddTransient<BoardPage, BoardViewModel>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
        builder.Services.AddSingleton<IHighScoreService, HighScoreService>();
        builder.Services.AddSingleton<IAudioManager>(AudioManager.Current);
        builder.Services.AddSingleton<ISoundService>(sp =>
        {
            var am = sp.GetRequiredService<IAudioManager>();
            var sm = new SoundService(am);
            sm.Init();
            return sm;
        });

        return builder.Build();
    }
}
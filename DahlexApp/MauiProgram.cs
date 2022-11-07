using CommunityToolkit.Maui;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Settings;
using DahlexApp.Views.How;
using DahlexApp.Views.Start;
using DahlexApp.Logic.Services;
using DahlexApp.Views.Board;
using DahlexApp.Views.Scores;
using DahlexApp.Views.Settings;
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

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<HowPage>();
		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<ScoresPage>();
		builder.Services.AddTransient<BoardPage>();


        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
        builder.Services.AddSingleton<IHighScoreService, HighScoreService>();
        builder.Services.AddSingleton<IAudioManager>(AudioManager.Current);

        builder.Services.AddTransient<StartViewModel>();
        builder.Services.AddTransient<HowViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<ScoresViewModel>();
        builder.Services.AddTransient<BoardViewModel>();


        return builder.Build();
	}
}

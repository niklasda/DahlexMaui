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
using Microsoft.Extensions.DependencyInjection;

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

    //    builder.Services.AddSingleton<MainPage>();
//		builder.Services.AddTransient<HowPage>();
	//	builder.Services.AddTransient<SettingsPage>();
		//builder.Services.AddTransient<ScoresPage>();
	//	builder.Services.AddTransient<BoardPage>();



        builder.Services.AddTransient<MainPage, StartViewModel>();
        builder.Services.AddTransient<HowPage, HowViewModel>();
        builder.Services.AddTransient<SettingsPage, SettingsViewModel>();
        builder.Services.AddTransient<ScoresPage, ScoresViewModel>();
        builder.Services.AddTransient<BoardPage, BoardViewModel>();


        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
        builder.Services.AddSingleton<IHighScoreService, HighScoreService>();
        builder.Services.AddSingleton<IAudioManager>(AudioManager.Current);
        builder.Services.AddSingleton<ISoundManager>(  sp => { var am = sp.GetRequiredService<IAudioManager>();
			var sm = new SoundManager(am);
			 sm.Init();
			return sm;
		});

        //        builder.Services.AddTransient<StartViewModel>();
        //      builder.Services.AddTransient<HowViewModel>();
        //    builder.Services.AddTransient<SettingsViewModel>();
        //  builder.Services.AddTransient<ScoresViewModel>();
        //builder.Services.AddTransient<BoardViewModel>();


        return builder.Build();
	}
}

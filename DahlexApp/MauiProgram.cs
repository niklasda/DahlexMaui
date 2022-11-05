﻿using DahlexApp.Views;
using CommunityToolkit.Maui;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Settings;
using DahlexApp.Views.How;
using DahlexApp.Views.Start;
using DahlexApp.Logic.Services;
using DahlexApp.Views.Scores;
using DahlexApp.Views.Settings;

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


        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
        builder.Services.AddSingleton<IHighScoreService, HighScoreService>();

        builder.Services.AddTransient<StartViewModel>();
        builder.Services.AddTransient<HowViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<ScoresViewModel>();


        return builder.Build();
	}
}

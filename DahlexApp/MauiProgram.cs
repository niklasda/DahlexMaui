using DahlexApp.Views;
﻿using CommunityToolkit.Maui;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Settings;
using DahlexApp.Views.How;

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
		builder.Services.AddTransient<NewPage1>();

        
        builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
        builder.Services.AddSingleton<IHighScoreService, HighScoreService>();
        builder.Services.AddTransient<HowViewModel>();
        builder.Services.AddTransient<HowViewModel>();


        return builder.Build();
	}
}

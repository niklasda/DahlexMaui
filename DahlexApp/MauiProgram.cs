<<<<<<< HEAD
﻿using DahlexApp.Views;
=======
﻿using CommunityToolkit.Maui;
>>>>>>> de3d8a7b9d9f21498e9a0cd2425aaafa8fca870d

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

        return builder.Build();
	}
}

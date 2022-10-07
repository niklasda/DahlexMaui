﻿using DahlexApp.Views;
using DahlexApp.Views.How;

namespace DahlexApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";
        
        Navigation.PushAsync(new NewPage1());

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


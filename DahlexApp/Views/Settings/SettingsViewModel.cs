﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Settings;
using JetBrains.Annotations;

namespace DahlexApp.Views.Settings;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
public partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel(INavigationService navigationService)
    {
        BackCommand = new AsyncRelayCommand(navigationService.NavigateBack);
        CloseImage = ImageSource.FromFile("close.png");

        Title = "Settings";

        SettingsManager sm = new SettingsManager(new IntSize(0, 0));

        Copyright = "Dahlex v0.9 (c) 2023 nida";

        var gs = sm.LoadLocalSettings();
        ProfName = gs.PlayerName;
        IsMuted = gs.LessSound;
    }

    public void OnDisappearing()
    {
        SettingsManager sm = new SettingsManager(new IntSize(0, 0));
        var g = new GameSettings(new IntSize(0, 0));

        g.PlayerName = ProfName;
        g.LessSound = IsMuted;

        sm.SaveLocalSettings(g);
    }

    public IAsyncRelayCommand BackCommand { get; set; }

    public ImageSource CloseImage { get; set; }

    [ObservableProperty]
    private string _title;

    //public string Title
    //{
    //    get => _title;
    //    set => SetProperty(ref _title, value);
    //}

    [ObservableProperty]
    private string _profName;

    //public string ProfName
    //{
    //    get => _profName;
    //    set => SetProperty(ref _profName, value);
    //}

    [ObservableProperty]
    private string _copyright;

    //public string Copyright
    //{
    //    get => _copyright;
    //    set => SetProperty(ref _copyright, value);
    //}

    [ObservableProperty]
    private bool _isMuted;

    //public bool IsMuted
    //{
    //    get => _isMuted;
    //    set => SetProperty(ref _isMuted, value);
    //}
}
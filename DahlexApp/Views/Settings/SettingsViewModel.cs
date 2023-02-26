using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Services;
using DahlexApp.Logic.Settings;

namespace DahlexApp.Views.Settings;

public class SettingsViewModel : ObservableObject
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

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _profName = string.Empty;
    public string ProfName
    {
        get => _profName;
        set => SetProperty(ref _profName, value);
    }

    private string _copyright = string.Empty;
    public string Copyright
    {
        get => _copyright;
        set => SetProperty(ref _copyright, value);
    }

    private bool _isMuted;
    public bool IsMuted
    {
        get => _isMuted;
        set => SetProperty(ref _isMuted, value);
    }
}

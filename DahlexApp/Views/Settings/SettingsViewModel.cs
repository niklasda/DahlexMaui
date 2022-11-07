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

            _title = string.Empty;
            Title = "Settings";

            SettingsManager sm = new SettingsManager(new IntSize(0, 0));

            _profName = "";
            
            var gs = sm.LoadLocalSettings();
            ProfName = gs.PlayerName;
            IsMuted = gs.LessSound;
        }



        //public override void Prepare()
        //{
        //    // first callback. Initialize parameter-agnostic stuff here
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();

        //    // do the heavy work here

        //    SettingsManager sm = new SettingsManager(new Size(0,0));
        //    var gs = sm.LoadLocalSettings();
        //    ProfName = gs.PlayerName;
        //    IsMuted = gs.LessSound;
        //}

        //public override void ViewDisappeared()
        //{
        //    base.ViewDisappeared();

        //    SettingsManager sm = new SettingsManager(new Size(0, 0));
        //    sm.SaveLocalSettings(new GameSettings(new Size(0,0) ){PlayerName = ProfName, LessSound = IsMuted});
        //}

        public IAsyncRelayCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _profName;
        public string ProfName
        {
            get => _profName;
            set => SetProperty(ref _profName, value);
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set => SetProperty(ref _isMuted, value);
        }
    }

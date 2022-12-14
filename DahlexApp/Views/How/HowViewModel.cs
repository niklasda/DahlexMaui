using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Services;

namespace DahlexApp.Views.How
{
    public class HowViewModel : ObservableObject
    {
        public HowViewModel(INavigationService navigationService)
        {
            //  _dispatcher = dispatcher;
            //_score = score;
            //  INavigation navigation = App.Current.MainPage.Navigation;
            //   BackCommand = new MvxCommand(() => { _ = Task.Run(async () => await navigationService.Close(this)); });
            CloseImage = ImageSource.FromFile("close.png");

            _title = string.Empty;
         //   _playerName = string.Empty;

            Title = "How";
         //   PlayerName = "nIX";
         //   IsMuted = false;

            BackCommand = new AsyncRelayCommand( navigationService.NavigateBack);

            // ImageSource image; 
            //   Assembly assembly = GetType().GetTypeInfo().Assembly;

            //MainThread.BeginInvokeOnMainThread();

            HowToPages.Clear();
            // HowToPages.Add(new HowItemViewModel { ImageText = "Simple", ImageSource = ImageSource.FromResource("DahlexApp.Properties.Resources.resources.screen1_1280.png") });
            HowToPages.Add(new HowItemViewModel("Simple", ImageSource.FromFile("screen4_1280.png")));
            HowToPages.Add(new HowItemViewModel("Who is who", ImageSource.FromFile("screen2_1280.png")));
            HowToPages.Add(new HowItemViewModel("Busy", ImageSource.FromFile("screen4_1280.png")));
            //      AwaitKt Shell.Current.GoToAsync();
        }


        //private readonly IMvxMainThreadAsyncDispatcher _dispatcher;
       // private IHighScoreService _score;

        //public override void Prepare()
        //{
        //    // first callback. Initialize parameter-agnostic stuff here
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();

        //    // do the heavy work here
        //}

         
     
        public ObservableCollection<HowItemViewModel> HowToPages { get; } = new ObservableCollection<HowItemViewModel>();

//        public override void ViewAppeared()
  //      {
           // base.ViewAppeared();

            //_ = _dispatcher.ExecuteOnMainThreadAsync(() =>
            //{

            //    Title = "How";

            //    HowToPages.Clear();
            //    HowToPages.Add(new HowItemViewModel { ImageText = "Simple", ImageSource = ImageSource.FromResource("DahlexApp.Assets.Screens.Screen1_1280.png") });
            //    HowToPages.Add(new HowItemViewModel { ImageText = "Who is who", ImageSource = ImageSource.FromResource("DahlexApp.Assets.Screens.Screen2_1280.png") });
            //    HowToPages.Add(new HowItemViewModel { ImageText = "Busy", ImageSource = ImageSource.FromResource("DahlexApp.Assets.Screens.Screen4_1280.png") });

            //});
            // 
    //    }

        public IAsyncRelayCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }

        private string _title ;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value) ;
        }

        //private string _playerName;
        //public string PlayerName
        //{
        //    get => _playerName;
        //    set => SetProperty(ref _playerName, value);
        //}

        //private bool _isMuted;
        //public bool IsMuted
        //{
        //    get => _isMuted;
        //    set => SetProperty(ref _isMuted, value);
        //}
    }
}

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Settings;

namespace DahlexApp.Views.How
{
    public class HowViewModel : ObservableObject
    {
        public HowViewModel(IHighScoreService score)
        {
            //  _dispatcher = dispatcher;
            _score = score;
            //  INavigation navigation = App.Current.MainPage.Navigation;
            //   BackCommand = new MvxCommand(() => { _ = Task.Run(async () => await navigationService.Close(this)); });
            CloseImage = ImageSource.FromResource("DahlexApp.Assets.Images.Close.png");

            Title = "How";
            PlayerName = "nIX";
            IsMuted = false;

            BackCommand = new RelayCommand(() => { _ = Task.Run(() => Navigation.PopAsync()); });

            //      AwaitKt Shell.Current.GoToAsync();
        }


        //private readonly IMvxMainThreadAsyncDispatcher _dispatcher;
        private IHighScoreService _score;

        //public override void Prepare()
        //{
        //    // first callback. Initialize parameter-agnostic stuff here
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();

        //    // do the heavy work here
        //}

        protected INavigation Navigation
        {
            get
            {
                INavigation? navigation = Application.Current?.MainPage?.Navigation;
                if (navigation is not null)
                    return navigation;
                else
                {
                    //This is not good!
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        }

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

        public IRelayCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value) ;
        }

        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => _playerName = value;
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set => _isMuted = value;
        }
    }
}

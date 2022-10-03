﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DahlexApp.Views.How
{
    public class HowViewModel 
    {
        public HowViewModel()
        {
          //  _dispatcher = dispatcher;

         //   BackCommand = new MvxCommand(() => { _ = Task.Run(async () => await navigationService.Close(this)); });
            CloseImage = ImageSource.FromResource("DahlexApp.Assets.Images.Close.png");


      //      AwaitKt Shell.Current.GoToAsync();
        }

        //private readonly IMvxMainThreadAsyncDispatcher _dispatcher;


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

       // public IMvxCommand BackCommand { get; set; }

        public ImageSource CloseImage { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => _title = value;
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

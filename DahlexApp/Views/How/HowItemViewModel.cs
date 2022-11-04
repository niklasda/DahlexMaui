//using Xamarin.Forms;

using CommunityToolkit.Mvvm.ComponentModel;
using JetBrains.Annotations;

namespace DahlexApp.Views.How
{
  //  [UsedImplicitly]
    public class HowItemViewModel : ObservableObject
    {
        private string _imageText= string.Empty;
        public string ImageText
        {
            get => _imageText;
            set => _imageText = value;
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }
    }
}

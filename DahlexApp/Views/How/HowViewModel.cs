using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DahlexApp.Logic.Services;
using System.Collections.ObjectModel;

namespace DahlexApp.Views.How;

public class HowViewModel : ObservableObject
{
    public HowViewModel(INavigationService navigationService)
    {
        CloseImage = ImageSource.FromFile("close.png");

        _title = string.Empty;

        Title = "How";

        BackCommand = new AsyncRelayCommand(navigationService.NavigateBack);

        HowToPages.Clear();
        HowToPages.Add(new HowItemViewModel("Simple", ImageSource.FromFile("screen4_1280.png")));
        HowToPages.Add(new HowItemViewModel("Who is who", ImageSource.FromFile("screen2_1280.png")));
        HowToPages.Add(new HowItemViewModel("Busy", ImageSource.FromFile("screen4_1280.png")));
    }

    public ObservableCollection<HowItemViewModel> HowToPages { get; } = new ObservableCollection<HowItemViewModel>();

    public IAsyncRelayCommand BackCommand { get; set; }

    public ImageSource CloseImage { get; set; }

    private string _title;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
}
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;

namespace DahlexApp.Views.Board;

public partial class BoardPage : IBoardPage
{
    public BoardPage(BoardViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);

        vm.TheAbsBoard = TheBoard;
        vm.TheAbsOverBoard = TheOverBoard;

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            // crashes on windows on dell, but not on Lenovo
            DeviceDisplay.Current.KeepScreenOn = true;
        }
    }

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is BoardViewModel vm)
        {
            if (vm.AttemptBack())
            {
                return base.OnBackButtonPressed();
            }
            return true;
        }
        //       else
        //     {
        return base.OnBackButtonPressed();
        //   }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is BoardViewModel vm)
        {
            vm.OnDisappearing();
        }
    }

    public async Task SetStartGameMode(GameMode value)
    {
        if (BindingContext is BoardViewModel vm)
        {
            await vm.SetStartGameMode(value);

            await vm.OnAppearing();
        }
    }
}
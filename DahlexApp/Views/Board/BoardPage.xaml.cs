using DahlexApp.Logic.Models;
using DahlexApp.Logic.Services;

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
            vm.StartGameMode = value;


            await vm.OnAppearing();
        }
    }

    public GameMode StartGameMode
    {

        set
        {
            if (BindingContext is BoardViewModel vm)
            {
                vm.StartGameMode = value;


                vm.OnAppearing().GetAwaiter().GetResult();
            }
        }
    }
}

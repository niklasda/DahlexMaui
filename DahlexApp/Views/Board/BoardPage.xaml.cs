﻿using DahlexApp.Logic.Models;
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

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            DeviceDisplay.Current.KeepScreenOn = true;
    }

    protected override bool OnBackButtonPressed()
    {
        var vm = BindingContext as BoardViewModel;
        if (vm != null)
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
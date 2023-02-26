using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DahlexApp.Logic.Game;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Logger;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Services;
using DahlexApp.Logic.Settings;
using DahlexApp.Logic.Utils;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Diagnostics;

namespace DahlexApp.Views.Board;

public class BoardViewModel : ObservableObject, IDahlexView, IBoardPage
{

    public BoardViewModel(IHighScoreService hsm, ISoundService audio)
    {
        _settings = GetSettings();
        _ge = new GameEngine(_settings, this, hsm);
       
        _audio = audio;
        // w411 h660

        _title = "";
        _timerText = "0s";
        _bombText = "B";
        _teleText = "T";
        _infoText = "";
        _infoText1 = "Level:";
        _infoText2 = "Dahlex:";

        Title = "Play";



        StartGameCommand = new AsyncRelayCommand(async () =>
        {
            _gameTimer?.Stop();
            _elapsed = TimeSpan.Zero;

            _gameTimer = Application.Current.Dispatcher.CreateTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _gameTimer.Tick += GameTimerElapsed;
            _gameTimer.Start();

            await _ge.StartGame(GameMode.Random);
            UpdateUi(GameStatus.GameStarted, _ge.GetState(_elapsed));
        });



        NextLevelCommand = new AsyncRelayCommand(async () =>
        {
            if (_ge != null)
            {
                if (_ge.Status == GameStatus.LevelComplete)
                {
                    //storyPanel.Resources.Clear();

                    await _ge.StartNextLevel();

                    _gameTimer?.Start();
                }

                UpdateUi(_ge.Status, _ge.GetState(_elapsed));
            }
        });

        BombCommand = new AsyncRelayCommand(async () =>
        {
            try
            {
                await BlowBomb();

            }
            catch (Exception)
            {
            }
        });

        TeleCommand = new AsyncRelayCommand(DoTeleport);


        //  OnAppearing().GetAwaiter().GetResult();
    }




    private async Task DoTeleport()
    {
        //  await Application.Current.MainPage.DisplayAlert("Dahlex", "Coming SoOon", "Ok");

        if (_ge != null)
        {

            if (_ge.Status == GameStatus.LevelOngoing)
            {
                await _ge.MoveHeapsToTemp();
                if (await _ge.DoTeleport())
                {
                    PlaySound(Sound.Teleport);

                    await _ge.MoveRobotsToTemp();
                    await _ge.CommitTemp();
                }
                else
                {
                    AddLineToLog("No more teleports");
                }
            }

            UpdateUi(_ge.Status, _ge.GetState(_elapsed));
        }
    }

    private async Task BlowBomb()
    {
        if (_ge != null)
        {
            if (_ge.Status == GameStatus.LevelOngoing)
            {
                await _ge.MoveHeapsToTemp();
                if (await _ge.BlowBomb())
                {
                    try
                    {
                        // do not await
                        DrawExplosionRadius(_ge.GetProfessorCoordinates());
                    }
                    catch (Exception ex)
                    {
                        GameLogger.AddLineToLog(ex.Message);
                        // Debug.WriteLine();
                        // safety try, marketplace version crashes on samsung 
                        //MessageBox.Show(ex.Message);
                    }

                    if (Vibration.Default.IsSupported)
                    {
                        Vibration.Default.Vibrate();
                    }

                    PlaySound(Sound.Bomb);
                    if (await _ge.MoveProfessorToTemp(MoveDirection.None))
                    {
                        await _ge.MoveRobotsToTemp();
                        await _ge.CommitTemp();
                    }
                }
                else
                {
                    IToast m = Toast.Make("Nothing to bomb");
                    await m.Show();

                    //_toast.ShowToastMessage("Nothing to bomb");
                    AddLineToLog("Cannot bomb");
                }
            }

            UpdateUi(_ge.Status, _ge.GetState(_elapsed));
        }
    }

    private async Task DrawExplosionRadius(IntPoint pos)
    {
        Color borderColor = Color.FromRgb(0x53, 0xc0, 0x90);
        if (Application.Current.Resources.TryGetValue("GreenAccentColor", out var bgc))
        {
            borderColor = (Color)bgc;
        }

        var bv = new Ellipse()
        {
            Fill = new SolidColorBrush(Colors.Transparent),
            Stroke = new SolidColorBrush(borderColor),
            StrokeThickness = 1,

        };

        AbsoluteLayout.SetLayoutBounds(bv, new Rect(37 * pos.X + 16, 37 * pos.Y + 16, 5, 5));
        AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.None);

        TheAbsBoard.Children.Add(bv);

        var t1 = bv.ScaleXTo(15, 400);
        await t1;
        var t2 = bv.ScaleYTo(15, 300);
        await t2;

        var t3 = bv.ScaleXTo(0.1, 1000);
        await t3;
        var t4 = bv.ScaleYTo(0.1, 7000);

        await t4;
        TheAbsBoard.Children.Remove(bv);
    }


    private async Task<bool> PerformRound(MoveDirection dir)
    {
        bool movedOk = false;

        if (_ge != null)
        {
            if (_ge.Status == GameStatus.LevelOngoing)
            {
                await _ge.MoveHeapsToTemp();
                if (await _ge.MoveProfessorToTemp(dir))
                {
                    await _ge.MoveRobotsToTemp();
                    await _ge.CommitTemp();

                    movedOk = true;
                }
                else
                {
                    AddLineToLog("P. not moved");
                }
            }

            UpdateUi(_ge.Status, _ge.GetState(_elapsed));
        }
        return movedOk;
    }

    private GameSettings GetSettings()
    {
        //w411 h660    iw37 37*11=407   37*13=481

        var width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density; //1440
        var height = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density; //2560

        ShortestDimension = Math.Min((int)width, (int)height);
        HeightDimension = ShortestDimension + 37 * 2;

        ISettingsManager sm = new SettingsManager(new IntSize(37 * 11, 37 * 13)); // w11 h13
        var s = sm.LoadLocalSettings();
        return s;
    }

    private void GameTimerElapsed(object? sender, EventArgs eventArgs)
    {
        if (_ge.Status == GameStatus.LevelOngoing)
        {
            _elapsed = _elapsed.Add(new TimeSpan(0, 0, 1));
            TimerText = $"{_elapsed.TotalSeconds}s";
        }
        else
        {
            _gameTimer.Stop();
        }
    }

    private bool _canBomb;
    public bool CanBomb
    {
        get => _canBomb;
        set => SetProperty(ref _canBomb, value);
    }

    private bool _canTele;
    public bool CanTele
    {
        get => _canTele;
        set => SetProperty(ref _canTele, value);
    }

    private bool _canStart;
    public bool CanStart
    {
        get => _canStart;
        set => SetProperty(ref _canStart, value);
    }

    private bool _canNext;
    public bool CanNext
    {
        get => _canNext;
        set => SetProperty(ref _canNext, value);
    }


    private readonly GameSettings _settings;
    private readonly IGameEngine _ge;
    private GameMode StartGameMode { get; set; }
    public async Task SetStartGameMode(GameMode value)
    {
        StartGameMode = value;
    }


    public IAsyncRelayCommand BombCommand { get; }
    public IAsyncRelayCommand TeleCommand { get; }
    public IAsyncRelayCommand NextLevelCommand { get; }
    public IAsyncRelayCommand StartGameCommand { get; }


    private TimeSpan _elapsed = TimeSpan.Zero;

    private void UpdateUi(GameStatus gameStatus, IGameState state)

    {
        // is all the main thread things needed
        // MainThread.BeginInvokeOnMainThread(() =>
        // {

        if (gameStatus == GameStatus.BeforeStart)
        {
            CanBomb = false;
            CanTele = false;
            CanNext = false;
            CanStart = true;
        }
        else if (gameStatus == GameStatus.GameStarted)
        {
            //AddLineToLog("Game started");
            CanBomb = true;
            CanTele = true;
            CanNext = false;
            CanStart = false;

            if (state.Level == 1)
            {
                AddLineToLog("Game started");
            }
            else
            {
                AddLineToLog("Level started");
            }
        }
        else if (gameStatus == GameStatus.LevelComplete)
        {

            AddLineToLog("Level won");

            CanBomb = false;
            CanTele = false;
            CanNext = true;
            CanStart = false;

            //InfoText = state.Message;
        }
        else if (gameStatus == GameStatus.LevelOngoing)
        {
            if (state.BombCount > 0)
            {
                CanBomb = true;
            }

            if (state.TeleportCount > 0)
            {
                CanTele = true;
            }

            CanNext = false;
            CanStart = false;

            if (string.IsNullOrWhiteSpace(state.Message))
            {
                AddLineToLog("Game started");
            }
            else
            {
                InfoText = state.Message;
            }

            //InfoText = state.Message;
        }
        else if (gameStatus == GameStatus.GameLost)
        {
            AddLineToLog("You lost");
            CanBomb = false;
            CanTele = false;
            CanNext = false;
            CanStart = true;
        }
        else if (gameStatus == GameStatus.GameWon)
        {
            // tutorial won
            // 
            AddLineToLog("Tutorial won");
            CanBomb = false;
            CanTele = false;
            CanNext = false;
            CanStart = true;

        }

        if (state.BombCount < 1)
        {
            CanBomb = false;
        }

        if (state.TeleportCount < 1)
        {
            CanTele = false;
        }
        // });
    }



    private IDispatcherTimer _gameTimer;

    public async Task OnAppearing()
    {
        // base.ViewAppeared();

        if (_ge.Status == GameStatus.BeforeStart)
        {
            await _ge.StartGame(StartGameMode);

        }

        // todo save and load state

        UpdateUi(_ge.Status, _ge.GetState(_elapsed));

        for (int x = 0; x < 11; x++)
        {
            for (int y = 0; y < 13; y++)
            {
                BoxView bv = new BoxView();

                if (x % 2 == 0 && y % 2 == 1 || x % 2 == 1 && y % 2 == 0)
                {
                    
                    bv.Color = Colors.Orange;
                }
                else
                {
                    bv.Color = Colors.DarkOrange;

                }


                AbsoluteLayout.SetLayoutBounds(bv, new Rect(37 * x, 37 * y, 37, 37));
                AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.None);
                TheAbsBoard.Children.Add(bv);
            }
        }


        //Debug.WriteLine($"Registering Weak Messenger");

        WeakReferenceMessenger.Default.Register<BoardViewModel, string>(this, async (a, dir) =>
        {
            //Debug.WriteLine($" Weak Messenger");

            if (Enum.TryParse<MoveDirection>(dir, true, out MoveDirection md))
            {
                Debug.WriteLine($"Pan: {md}");

                bool moved = await PerformRound(md);

            }

        });

        var pan = new PanGestureRecognizer();
        pan.PanUpdated += Pan_PanUpdated;

        var tap = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
        //tap.Tapped += (o, e) => { };
        tap.Command = new RelayCommand(() =>
        {

            Debug.WriteLine($"Tap: {MoveDirection.None}");
            WeakReferenceMessenger.Default.Send<string>(MoveDirection.None.ToString());

        }
       );

        
        TheAbsOverBoard.GestureRecognizers.Clear();
     
        TheAbsOverBoard.GestureRecognizers.Add(tap);
        TheAbsOverBoard.GestureRecognizers.Add(pan);
      
    }

    private void Tap_Tapped(object? sender, TappedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private int _tempX;
    private int _tempY;

    private void Pan_PanUpdated(object? sender, PanUpdatedEventArgs e)
    {


        if (e.StatusType == GestureStatus.Started)
        {
            Debug.WriteLine($"{e.GestureId}: {e.StatusType}");
            _tempX = 0;
            _tempY = 0;
        }
        else if (e.StatusType == GestureStatus.Running)
        {
            _tempX = (int)e.TotalX;
            _tempY = (int)e.TotalY;
        }
        else if (e.StatusType == GestureStatus.Canceled)
        {
            Debug.WriteLine($"{e.GestureId}: {e.StatusType}");
            //_tempX = e.TotalX;
            //_tempY = e.TotalY;
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            Debug.WriteLine($"{e.GestureId}: {e.StatusType}");
            //bool moved;
            var p = new Point(_tempX, _tempY);

            if (_ge != null && _ge.Status == GameStatus.LevelOngoing)
            {
                // Point p = new Point(e.TotalManipulation.Translation.X, e.TotalManipulation.Translation.Y);
                // very small swipe or tap is like clicking the professor in standard move mode
                if (IsTap(p))
                {
                    //WeakReferenceMessenger.Default.Send<string>(MoveDirection.None.ToString());

                    //  moved = await PerformRound(MoveDirection.None);
                }
                else //if (IsWithinBounds(e))
                {
                    var direction = Trig.GetSwipeDirection(p);
                    if (direction != MoveDirection.Ignore)
                    {
                        WeakReferenceMessenger.Default.Send<string>(direction.ToString());

                        //    moved = await PerformRound(direction);
                    }
                }
            }
        }
    }

    private static bool IsTap(Point p)
    {
        return Trig.IsTooSmallSwipe(p);
    }

    public void OnDisappearing()
    {
        WeakReferenceMessenger.Default.Unregister<string>(this);

        _gameTimer?.Stop();
    }

    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _infoText;
    public string InfoText
    {
        get => _infoText;
        set => SetProperty(ref _infoText, value);
    }

    private string _timerText;
    public string TimerText
    {
        get => _timerText;
        set => SetProperty(ref _timerText, value);
    }

    private string _infoText1;
    public string InfoText1
    {
        get => _infoText1;
        set => SetProperty(ref _infoText1, value);
    }

    private string _infoText2;
    public string InfoText2
    {
        get => _infoText2;
        set => SetProperty(ref _infoText2, value);
    }

    private string _bombText;
    public string BombText
    {
        get => _bombText;
        set => SetProperty(ref _bombText, value);
    }

    private string _teleText;
    public string TeleText
    {
        get => _teleText;
        set => SetProperty(ref _teleText, value);
    }

    private int _shortestDimension;
    public int ShortestDimension
    {
        get => _shortestDimension;
        set => SetProperty(ref _shortestDimension, value);
    }

    private int _heightDimension;
    public int HeightDimension
    {
        get => _heightDimension;
        set => SetProperty(ref _heightDimension, value);
    }


    private bool _isBusy;
    private readonly ISoundService _audio;

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }


    public AbsoluteLayout TheAbsBoard { get; set; }
    public AbsoluteLayout TheAbsOverBoard { get; set; }
    public void AddLineToLog(string log)
    {
        GameLogger.AddLineToLog(log);
        InfoText = log;

    }

    public async Task DrawBoard(IBoard board, int xSize, int ySize)
    {
        //  var s = Application.Current.Dispatcher(() => { });

        // await MainThread.InvokeOnMainThreadAsync(async () =>
        // {


        for (int x = 0; x < board.GetPositionWidth(); x++)
        {
            for (int y = 0; y < board.GetPositionHeight(); y++)
            {
                BoardPosition cp = board.GetPosition(x, y);
                if (cp != null)
                {

                    string imgName;
                    if (cp.Type == PieceType.Heap)
                    {
                        Image boardImage = new Image { InputTransparent = true };



                        imgName = cp.ImageName;
                        // Robot2ImageSource = ImageSource.FromResource("DahlexApp.Assets.Images.heap_02.png");
                        boardImage.AutomationId = imgName;
                        Debug.WriteLine($"{cp.Type} AutomationId set to {imgName}");
                        boardImage.Source = ImageSource.FromFile("heap_02.png");

                        TheAbsOverBoard.Children.Add(boardImage);

                        await Animate(cp, new IntPoint(0, 0), new IntPoint(x, y), 250);

                        // boardImage = pic;
                        // Image img = AddImage(imgName, boardImage, pt, cp);
                        if (cp.IsNew)
                        {
                            //   AddToFade(img, 0, 1);
                            cp.IsNew = false;
                        }
                    }
                    else if (cp.Type == PieceType.Professor)
                    {
                        Image boardImage = new Image { InputTransparent = true };


                        imgName = cp.ImageName;
                        // boardImage.Source = LoadImage("planet_01.png");

                        //boardImage.SetValue(BindablePropertyKey.FrameworkElement.NameProperty, imgName);
                        boardImage.AutomationId = imgName;
                        Debug.WriteLine($"{cp.Type} AutomationId set to {imgName}");
                        boardImage.Source = ImageSource.FromFile("planet_01.png");
                        TheAbsOverBoard.Children.Add(boardImage);
                        //boardImage = pic;
                        //AddImage(imgName, boardImage, pt, cp);
                        await Animate(cp, new IntPoint(0, 0), new IntPoint(x, y), 250);

                    }
                    else if (cp.Type == PieceType.Robot)
                    {
                        Image boardImage = new Image { InputTransparent = true };


                        imgName = cp.ImageName;
                        string name = Randomizer.GetRandomFromSet("robot_04.png", "robot_05.png", "robot_06.png");
                        boardImage.Source = ImageSource.FromFile(name);
                        boardImage.AutomationId = imgName;
                        Debug.WriteLine($"{cp.Type} AutomationId set to {imgName}");
                        //                         boardImage.Source = LoadImage(name);
                        TheAbsOverBoard.Children.Add(boardImage);

                        Animate(cp, new IntPoint(0, 0), new IntPoint(x, y), 250);

                        //     pic.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        //   boardImage = pic;
                        // AddImage(imgName, boardImage, pt, cp);
                    }
                    //else if (cp.Type == PieceType.None)
                    //{
                    // imgName = cp.ImageName;
                    // RemoveImage(imgName);
                    //}

                }
            }
        }
        // });
    }

    public void ShowStatus(int level, int bombCount, int teleportCount, int robotCount, int moveCount, int maxLevel)
    {
        InfoText1 = $"Level: {level}/{maxLevel} ";
        InfoText2 = $"Dahlex: {robotCount}  Moves: {moveCount}";
        BombText = $"Bomb ({bombCount})";
        TeleText = $"Tele ({teleportCount})";
    }

    public void Clear(bool all)
    {
        TheAbsOverBoard.Children.Clear();

    }

    public void PlaySound(Sound sound)
    {
        if (!_settings.LessSound)
        {

            switch (sound)
            {
                case Sound.Bomb:
                    PlayBomb();
                    break;
                case Sound.Teleport:
                    PlayTele();
                    break;
                case Sound.Crash:
                    PlayCrash();
                    break;
            }
        }
    }

    private void PlayBomb()
    {

        _audio.PlayBomb();

    }

    private void PlayTele()
    {
        _audio.PlayTele();
    }

    private void PlayCrash()
    {
        _audio.PlayCrash();

    }


    public async Task Animate(BoardPosition bp, IntPoint oldPosNotUsed, IntPoint newPos, uint millis)
    {
        // await MainThread.InvokeOnMainThreadAsync(async () =>
        //{


        int nLeft = newPos.X * (_settings.SquareSize.Width);
        int nTop = newPos.Y * (_settings.SquareSize.Height);

        if (bp.Type == PieceType.Professor)
        {

            IView i = TheAbsOverBoard.Children.First(z => z.AutomationId == bp.ImageName);
            VisualElement img = (VisualElement)i;

            // img.TranslationX = nLeft;
            // img.TranslationY = nTop;
            img.TranslateTo(nLeft, nTop, millis);

        }
        else if (bp.Type == PieceType.Robot)
        {
            var i = TheAbsOverBoard.Children.First(z => z.AutomationId == bp.ImageName);
            VisualElement img = (VisualElement)i;
            img.TranslateTo(nLeft, nTop, millis);
            // img.TranslationX = nLeft;
            // img.TranslationY = nTop;
        }
        else if (bp.Type == PieceType.Heap)
        {
            if (!TheAbsOverBoard.Children.Any(z => z.AutomationId == bp.ImageName))
            {
                Debug.WriteLine($"Could not find {bp.Type} with name {bp.ImageName}");
            }

            var i = TheAbsOverBoard.Children.First(z => bp.ImageName.Contains(z.AutomationId));
            VisualElement img = (VisualElement)i;
            //  await img.TranslateTo(nLeft, nTop, 0);
            img.TranslationX = nLeft;
            img.TranslationY = nTop;
        }
        //ud});
    }

    public void RemoveImage(string imageName)
    {
        //   MainThread.BeginInvokeOnMainThread(() =>
        // {

        var img = TheAbsOverBoard.Children.FirstOrDefault(z => z.AutomationId == imageName);
        TheAbsOverBoard.Children.Remove(img);
        // });
    }

    public void ChangeImage(BoardPosition bp)
    {
        // MainThread.BeginInvokeOnMainThread(() =>
        // {

        var imgv = TheAbsOverBoard.Children.FirstOrDefault(z => z.AutomationId == bp.ImageName);

        if (imgv is Image img)
        {
            TheAbsOverBoard.Children.Remove(imgv);
            img.Source = ImageSource.FromFile("heap_02.png");

            // move to front
            TheAbsOverBoard.Children.Add(imgv);

        }

        //            Image boardImage = new Image { InputTransparent = true };

        //          AbsoluteLayout.SetLayoutBounds(boardImage, new Rectangle(0 * x, 0 * y, 40, 40));
        //        AbsoluteLayout.SetLayoutFlags(boardImage, AbsoluteLayoutFlags.None);

        //      imgName = cp.ImageName;
        // boardImage.Source = LoadImage("planet_01.png");

        //boardImage.SetValue(BindablePropertyKey.FrameworkElement.NameProperty, imgName);
        //    boardImage.AutomationId = imgName;
        //  boardImage.Source = ImageSource.FromResource("DahlexApp.Assets.Images.planet_01.png");
        //TheAbsOverBoard.Children.Add(boardImage);
        // });
    }
}
//}

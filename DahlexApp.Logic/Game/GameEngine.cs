﻿using System.Text;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using DahlexApp.Logic.Settings;
using DahlexApp.Logic.Utils;

namespace DahlexApp.Logic.Game;

    public class GameEngine : IGameEngine
    {
        private readonly IDahlexView _boardView;

        private readonly IHighScoreService _highScoreManager;

        //    private readonly HighScoreManager _highScoreManager;
        private readonly GameSettings _settings;
        private int _bombCount;
        private int _teleportCount;
        private int _robotCount;
        private int _moveCount;
        private string _tail;

        private readonly IntSize _boardSize; // number of squares
        private readonly IntSize _squareSize; // in pixels
        private int _maxLevel;
        private IBoard _board;
        private DateTime _startTime;
        private GameMode _gameMode;

        public GameEngine(GameSettings settings, IDahlexView boardViewModel, IHighScoreService highScoreManager)
        {
            _tail = string.Empty;
            _boardView = boardViewModel;
            _highScoreManager = highScoreManager;
            //          _highScoreManager = highScoreManager;
            _settings = settings;

            _boardSize = _settings.BoardSize;
            _squareSize = _settings.SquareSize;
            _board = new BoardMatrix(IntSize.Empty);
        }

        public GameStatus Status { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool AreThereNoMoreLevels
        {
            get { return CurrentLevel >= _maxLevel; }
        }

        public async Task StartGame(GameMode mode)
        {
            _startTime = DateTime.Now;
            Status = GameStatus.LevelOngoing;

            const int startAt = 1; // don't change...

            CurrentLevel = startAt;
            _moveCount = startAt;
            _bombCount = startAt;
            _teleportCount = startAt;
            _gameMode = mode;

            if (_gameMode == GameMode.Random)
            {
                _maxLevel = _settings.MaxNumberOfLevel;
            }
            else if (_gameMode == GameMode.Campaign)
            {
                _maxLevel = Campaign1.Boards.Length - 1;
            }

            await InitNewLevel(CurrentLevel);
        }

        /// <summary>
        /// Continue game from tombstone, called after setState
        /// </summary>
        public async Task ContinueGame(GameMode mode)
        {
            _gameMode = mode;
            _startTime = DateTime.Now; // todo put in state

            await InitOldLevel(CurrentLevel);
        }

        /// <summary>
        /// Gather state to save to tombstone
        /// </summary>
        /// <param name="elapsed"></param>
        /// <returns></returns>
        public IGameState GetState(TimeSpan elapsed)
        {
            IGameState state = new GameState(_tail);
            state.Level = CurrentLevel;
            state.MoveCount = _moveCount;
            state.BombCount = _bombCount;
            state.TeleportCount = _teleportCount;
            state.GameStatus = (int)Status;
            state.ElapsedTimeInSeconds = (int)elapsed.TotalSeconds;
            state.Mode = (int)_gameMode;
            state.Message = _tail;

            if (_board != null) // if called before game is started
            {
                BoardPosition[,] tmp = _board.TheBoard;
                var b = new StringBuilder(_boardSize.Width * _boardSize.Height);

                for (int x = 0; x < _boardSize.Width; x++) // TODO this is run every round, seems inefficient
                {
                    for (int y = 0; y < _boardSize.Height; y++)
                    {
                        if (tmp[x, y] == null)
                        {
                            b.Append(' ');
                        }
                        else
                        {
                            string firstChar = tmp[x, y].Type.ToString().Substring(0, 1);
                            b.Append(firstChar);
                        }
                    }
                }

                state.TheBoard = b.ToString();
            }

            return state;
        }

        /// <summary>
        /// Restore state from tombstone, called before continueGame
        /// </summary>
        /// <param name="state"></param>
        public void SetState(IGameState state)
        {
            CurrentLevel = state.Level;
            _moveCount = state.MoveCount;
            _bombCount = state.BombCount;
            _teleportCount = state.TeleportCount;
            Status = (GameStatus)state.GameStatus;
            _gameMode = (GameMode)state.Mode;

            SetBoard(state.TheBoard);
        }

        private void SetBoard(string boardString)
        {
            var b = new BoardPosition[_boardSize.Width, _boardSize.Height];
            int i = 0;
            int heaps = 0;  
            int robots = 0;
            for (int x = 0; x < _boardSize.Width; x++)
            {
                for (int y = 0; y < _boardSize.Height; y = y + 1)
                {
                    if (boardString[i] == 'P')
                    {
                        b[x, y] = new BoardPosition(PieceType.Professor, "imgProfessor");
                    }
                    else if (boardString[i] == 'R')
                    {
                        b[x, y] = new BoardPosition(PieceType.Robot, "imgRobot" + robots++);
                    }
                    else if (boardString[i] == 'H')
                    {
                        b[x, y] = new BoardPosition(PieceType.Heap, "imgHeap" + heaps++);
                    }
                    else
                    {
                        //b[x, y] = null;
                    }
                    i++;
                }
            }

            _tail = boardString[i..];

            _board = new BoardMatrix(_boardSize);
            _board.TheBoard = b;
        }

        public async Task StartNextLevel()
        {
            if (CurrentLevel == _maxLevel)
            {
                // tutorial end
                //
                Status = GameStatus.GameWon;

                //_boardView.AddLineToLog("asd");
            }
            else
            {
                Status = GameStatus.LevelOngoing;
                CurrentLevel++;

                await InitNewLevel(CurrentLevel);
            }
        }

        /// <summary>
        /// Called from StartGame and startNextLevel
        /// </summary>
        /// <param name="thisLevel"></param>
        private async Task InitNewLevel(int thisLevel)
        {
            _bombCount++;
            _teleportCount++;

            if (_gameMode == GameMode.Campaign)
            {
                SetBoard(Campaign1.Boards[thisLevel]);

                _robotCount = _board.GetRobotCount();
            }
            else
            {
                _board = new BoardMatrix(_boardSize);
                _robotCount = thisLevel + 1;
                _tail = "";

                CreateProfessor();
                CreateRobots(_robotCount);
                CreateHeaps(thisLevel);
            }

            await Redraw(true);
        }

        /// <summary>
        /// Called from continueGame
        /// </summary>
        /// <param name="thisLevel"></param>
        private async Task InitOldLevel(int thisLevel)
        {
            if (_board == null) // i.e. not restored from tombstone
            {
                _robotCount = thisLevel + 1;
                _board = new BoardMatrix(_boardSize);
                CreateProfessor();
                CreateRobots(_robotCount);
                CreateHeaps(thisLevel);
            }
            else
            {
                _robotCount = _board.GetRobotCount();
            }

            await Redraw(true);
        }

        private void CreateProfessor()
        {
            IntPoint profPos = GetFreePosition();
            BoardPosition pPos = BoardPosition.CreateProfessorBoardPosition();
            _board.SetPosition(profPos.X, profPos.Y, pPos);
        }

        private void CreateRobots(int count)
        {
            RemoveOldPieces(PieceType.Heap);  // todo why, and why heaps
            for (int i = 0; i < count; i++)
            {
                IntPoint robotPos = GetFreePosition();
                BoardPosition rPos = BoardPosition.CreateRobotBoardPosition(i);
                _board.SetPosition(robotPos.X, robotPos.Y, rPos);
            }
        }

        private void CreateHeaps(int count)
        {
            RemoveOldPieces(PieceType.Heap); // todo why
            for (int i = 0; i < count; i++)
            {
                IntPoint robotPos = GetFreePosition();
                BoardPosition rPos = BoardPosition.CreateHeapBoardPosition(i);
                _board.SetPosition(robotPos.X, robotPos.Y, rPos);
            }
        }

        private void RemoveOldPieces(PieceType typeToRemove)
        {
            for (int x = 0; x < _board.GetPositionWidth(); x++)
            {
                for (int y = 0; y < _board.GetPositionHeight(); y++)
                {
                    if (_board.GetPosition(x, y) != null)
                    {
                        BoardPosition cp = _board.GetPosition(x, y);

                        if (cp.Type == typeToRemove)
                        {
                            _board.ResetPosition(x, y);
                        }
                    }
                }
            }
        }

        private IntPoint GetFreePosition()
        {
            IntPoint p;
            do
            {
                p = Randomizer.GetRandomPosition(_boardSize.Width, _boardSize.Height);
            }
            while (_board.GetPosition(p.X, p.Y) != null);

            return new IntPoint(p.X, p.Y);
        }

        public async Task MoveHeapsToTemp()
        {
            for (int x = 0; x < _board.GetPositionWidth(); x++)
            {
                for (int y = 0; y < _board.GetPositionHeight(); y++)
                {
                    if (_board.GetPosition(x, y) != null)
                    {
                        BoardPosition cp = _board.GetPosition(x, y);

                        if (cp.Type == PieceType.Heap)
                        {
                            IntPoint point = new IntPoint(x, y);

                            await MoveCharacter(point, point, 10); // todo  doesn't move
                        }
                    }
                }
            }
        }

        private async Task MoveCharacter(IntPoint oldPosition, IntPoint newPosition, uint millis)
        {
            BoardPosition oldBp = _board.GetPosition(oldPosition.X, oldPosition.Y);
            BoardPosition newBp = _board.GetTempPosition(newPosition.X, newPosition.Y);

            if (newBp == null || newBp.Type == PieceType.None)
            {
                _board.SetTempPosition(newPosition.X, newPosition.Y, oldBp);
                await _boardView.Animate(oldBp, oldPosition, newPosition, millis);
                _boardView.AddLineToLog($"M. {oldBp.Type} to {newPosition}");
            }
            else if (oldBp.Type == PieceType.Robot && newBp.Type == PieceType.Robot)
            {
                _boardView.AddLineToLog($"Robot-robot collision on {newPosition}");
                await _boardView.Animate(oldBp, oldPosition, newPosition, millis);

                await _boardView.PlaySound(Sound.Crash);
                _boardView.ChangeImage(newBp);
                newBp.ConvertToHeap();
                _robotCount -= 2;
                if (_robotCount == 0)
                {
                    Status = GameStatus.LevelComplete;
                }
            }
            else if (oldBp.Type == PieceType.Robot && newBp.Type == PieceType.Heap)
            {
                _boardView.AddLineToLog($"Robot-heap collision on {newPosition}");
                await _boardView.Animate(oldBp, oldPosition, newPosition, millis);

                await _boardView.PlaySound(Sound.Crash);
                _boardView.ChangeImage(newBp);

                newBp.ConvertToHeap();
                _robotCount--;
                if (_robotCount == 0)
                {
                    Status = GameStatus.LevelComplete;
                }
            }
            else if (oldBp.Type == PieceType.Robot && newBp.Type == PieceType.Professor)
            {
                _boardView.AddLineToLog($"Robot killed professor on {newPosition}");
                await _boardView.Animate(oldBp, oldPosition, newPosition, millis);

                await _boardView.PlaySound(Sound.Crash);

                newBp.ConvertToHeap();
                Status = GameStatus.GameLost;
                AddHighScore(false);
            }
            else if (oldBp.Type == PieceType.Professor && newBp.Type == PieceType.Robot)
            {
                _boardView.AddLineToLog($"Professor hit robot on {newPosition}");
                await _boardView.Animate(oldBp, oldPosition, newPosition, millis);

                await _boardView.PlaySound(Sound.Crash);

                newBp.ConvertToHeap();
                Status = GameStatus.GameLost;
                AddHighScore(false);
            }
            else if (oldBp.Type == PieceType.Professor && newBp.Type == PieceType.Heap)
            {
                _boardView.AddLineToLog($"Professor blocked by heap on {newPosition}");

                _board.SetTempPosition(oldPosition.X, oldPosition.Y, _board.GetPosition(oldPosition.X, oldPosition.Y));
            }
        }

        public IntPoint GetProfessorCoordinates()
        {
            IntPoint pos = GetProfessor(false);
            return pos;
        }

        private IntPoint GetProfessor(bool fromTemp)
        {
            if (fromTemp)
            {
                return _board.GetProfessorFromTemp();
            }
            else
            {
                return _board.GetProfessor();
            }
        }

        public async Task<bool> MoveProfessorToTemp(MoveDirection dir)
        {
            IntPoint oldProfessorPosition = GetProfessor(false);
            IntPoint newProfessorPosition = oldProfessorPosition;

            if (dir == MoveDirection.North)
            {
                if ((oldProfessorPosition.Y) > 0)
                {
                    newProfessorPosition.Y--;
                }
            }
            else if (dir == MoveDirection.East)
            {
                if ((oldProfessorPosition.X + 1) < _boardSize.Width)
                {
                    newProfessorPosition.X++;
                }
            }
            else if (dir == MoveDirection.South)
            {
                if ((oldProfessorPosition.Y + 1) < _boardSize.Height)
                {
                    newProfessorPosition.Y++;
                }
            }
            else if (dir == MoveDirection.West)
            {
                if ((oldProfessorPosition.X) > 0)
                {
                    newProfessorPosition.X--;
                }
            }
            else if (dir == MoveDirection.NorthEast)
            {
                if ((oldProfessorPosition.Y) > 0 && (oldProfessorPosition.X + 1) < _boardSize.Width)
                {
                    newProfessorPosition.Y--;
                    newProfessorPosition.X++;
                }
            }
            else if (dir == MoveDirection.SouthEast)
            {
                if ((oldProfessorPosition.Y + 1) < _boardSize.Height && (oldProfessorPosition.X + 1) < _boardSize.Width)
                {
                    newProfessorPosition.Y++;
                    newProfessorPosition.X++;
                }
            }
            else if (dir == MoveDirection.SouthWest)
            {
                if ((oldProfessorPosition.Y + 1) < _boardSize.Height && (oldProfessorPosition.X) > 0)
                {
                    newProfessorPosition.Y++;
                    newProfessorPosition.X--;
                }
            }
            else if (dir == MoveDirection.NorthWest)
            {
                if ((oldProfessorPosition.Y) > 0 && (oldProfessorPosition.X) > 0)
                {
                    newProfessorPosition.Y--;
                    newProfessorPosition.X--;
                }
            }
            else if (dir == MoveDirection.None)
            {
            }
            else
            {
                throw new Exception("No direction specified in move");
            }

            if (!oldProfessorPosition.Equals(newProfessorPosition) || (dir == MoveDirection.None))
            {
                await MoveCharacter(oldProfessorPosition, newProfessorPosition, 250); // no guid needed, prof has own storyboard
                _moveCount++;
                return true;
            }

            return false;
        }

        public async Task MoveRobotsToTemp()
        {
            IntPoint prof = GetProfessor(true);
            //var guid = Guid.NewGuid();

            for (int x = 0; x < _board.GetPositionWidth(); x++)
            {
                for (int y = 0; y < _board.GetPositionHeight(); y++)
                {
                    if (_board.GetPosition(x, y) != null)
                    {
                        BoardPosition cp = _board.GetPosition(x, y);
                        var current = new IntPoint(x, y);

                        if (cp.Type == PieceType.Robot)
                        {
                            var diff = new IntPoint(Math.Sign(prof.X - current.X), Math.Sign(prof.Y - current.Y));
                            var newPoint = new IntPoint(current.X + diff.X, current.Y + diff.Y);

                            await MoveCharacter(current, newPoint, 250);
                        }
                    }
                }
            }
            //_boardView.StartTheRobots(guid);
        }

        public async Task CommitTemp()
        {
            for (int x = 0; x < _board.GetPositionWidth(); x++)
            {
                for (int y = 0; y < _board.GetPositionHeight(); y++)
                {
                    var tempPosition = _board.GetTempPosition(x, y);

                    _board.SetPosition(x, y, tempPosition);
                    _board.ResetTempPosition(x, y);
                }
            }

            await Redraw(false);
        }

        public void AddHighScore(bool maxLevel)
        {
            // var sm = new SettingsManager();
            // var hsm = new HighScoreManager();

            string name = _settings.PlayerName;

            if (maxLevel)
            {
                CurrentLevel = _settings.MaxNumberOfLevel;
            }

            // _ = Task.Run(async () =>
            // {
            _highScoreManager.AddHighScore(_gameMode, name, CurrentLevel, _bombCount, _teleportCount, _moveCount, _startTime, _boardSize);
            _highScoreManager.SaveLocalHighScores();
            // });
        }

        private async Task Redraw(bool clear)
        {
            if (clear)
            {
                _boardView.Clear(true);


                //_boardView?.DrawLines();
                await _boardView.DrawBoard(_board, _squareSize.Width, _squareSize.Height);
            }
            _boardView?.ShowStatus(CurrentLevel, _bombCount, _teleportCount, _robotCount, _moveCount, _maxLevel);
        }

        public async Task<bool> BlowBomb()
        {
            int robotCountBefore = _robotCount;
            //Guid roundId = Guid.NewGuid();

            if (_bombCount > 0)
            {
                IntPoint prof = GetProfessor(false);

                for (int x = Math.Max(prof.X - 1, 0); x <= Math.Min(prof.X + 1, _boardSize.Width - 1); x++)
                {
                    for (int y = Math.Max(prof.Y - 1, 0); y <= Math.Min(prof.Y + 1, _boardSize.Height - 1); y++)
                    {
                        if (_board.GetPosition(x, y) != null)
                        {
                            BoardPosition bp = _board.GetPosition(x, y);

                            if (bp.Type == PieceType.Robot)
                            {
                                _boardView.AddLineToLog($"Bombing robot {x} {y}");
                                await _boardView.Animate(bp, new IntPoint(x, y), new IntPoint(x, y), 250);

                                //_boardView.RemoveRobotAnimation(bp);

                                _board.SetTempPosition(x, y, bp);
                                bp.ConvertToNone();
                                _boardView.RemoveImage(bp.ImageName);
                                //  }

                                _robotCount--;
                                if (_robotCount == 0)
                                {
                                    Status = GameStatus.LevelComplete;
                                }
                            }
                        }
                    }
                }
            }

            if (robotCountBefore != _robotCount)
            {
                _bombCount--;
                return true;
            }

            return false;
        }

        public async Task<bool> DoTeleport()
        {
            if (_teleportCount > 0)
            {
                IntPoint oldProfPos = GetProfessor(false);
                IntPoint newProfPos = GetFreePosition();

                _boardView.AddLineToLog($"T. from {oldProfPos} to {newProfPos}");

                await MoveCharacter(oldProfPos, newProfPos, 1500);
                _moveCount++;
                _teleportCount--;
                return true;
            }
            return false;
        }
    }

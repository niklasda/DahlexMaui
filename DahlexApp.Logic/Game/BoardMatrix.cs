﻿using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace DahlexApp.Logic.Game
{
    public sealed class BoardMatrix : IBoard
    {
        private BoardPosition[,] _positions;
        private readonly BoardPosition[,] _tempPositions;

        private readonly Size _boardSize;

        public BoardMatrix(Size pBoardSize)
        {
            _boardSize = pBoardSize;
            
            _positions = new BoardPosition[_boardSize.Width, _boardSize.Height];
            _tempPositions = new BoardPosition[_boardSize.Width, _boardSize.Height];
        }

        public BoardPosition[,] TheBoard
        {
            get { return _positions; }
            set { _positions = value; }
        }

        public int GetRobotCount()
        {
            int robotCount = 0;
            for (int x = 0; x < _boardSize.Width; x++)
            {
                for (int y = 0; y < _boardSize.Height; y++)
                {
                    if (_positions[x, y] != null)
                    {
                        BoardPosition cp = _positions[x, y];

                        if (cp.Type == PieceType.Robot)
                        {
                            robotCount++;
                        }
                    }
                }
            }

            return robotCount;
        }

        public BoardPosition GetPosition(int x, int y)
        {
            return _positions[x, y];
        }
        public void SetPosition(int x, int y, BoardPosition pos)
        {
            _positions[x, y] = pos;
        }
        public void ResetPosition(int x, int y)
        {
            _positions[x, y] = null;
        }
        public int GetPositionHeight()
        {
            return _boardSize.Height;
        }
        public int GetPositionWidth()
        {
            return _boardSize.Width;
        }
        public Point GetProfessor()
        {
            return GetProfessor(_positions);            
        }

        public BoardPosition GetTempPosition(int x, int y)
        {
            return _tempPositions[x, y];
        }
        public void SetTempPosition(int x, int y, BoardPosition pos)
        {
            _tempPositions[x, y] = pos;
        }
        public void ResetTempPosition(int x, int y)
        {
            _tempPositions[x, y] = null;
        }
        public Point GetProfessorFromTemp()
        {
            return GetProfessor(_tempPositions);
        }

        private Point GetProfessor(BoardPosition[,] bps)
        {
            for (int x = 0; x < _boardSize.Width; x++)
            {
                for (int y = 0; y < _boardSize.Height; y++)
                {
                    if (bps[x,y] != null)
                    {
                        BoardPosition cp = bps[x,y];

                        if (cp.Type == PieceType.Professor)
                        {
                            return new Point(x, y);
                        }
                    }
                }
            }

            return new Point(-1, -1);
        }
    }
}

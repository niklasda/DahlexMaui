﻿using System.Drawing;
using DahlexApp.Logic.Game;
using Size = System.Drawing.Size;
using DahlexApp.Logic.Models;
using Point = System.Drawing.Point;

namespace DahlexApp.Logic.Interfaces
{
    public interface IDahlexView
    {
        void AddLineToLog(string log);

        // void DrawGrid(int width, int height, int xSize, int ySize);
        void DrawBoard(IBoard board, int xSize, int ySize);

        void ShowStatus(int level, int bombCount, int teleportCount, int robotCount, int moveCount, int maxLevel);

        //   Control GetControlAt(IntPoint p);
        void Clear(bool all);

        // void SetBoardSizeControls();
        void PlaySound(Sound effect);

        void Animate(BoardPosition bp, Point oldPosition, Point newPosition, uint millis);

        // void RemoveAnimate(BoardPosition position);
        void RemoveImage(string imageName);

        void ChangeImage(BoardPosition bp);
    }
}

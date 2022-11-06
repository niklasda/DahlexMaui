using DahlexApp.Logic.Game;
using DahlexApp.Logic.Models;


namespace DahlexApp.Logic.Interfaces
{
    public interface IDahlexView
    {
        void AddLineToLog(string log);

        // void DrawGrid(int width, int height, int xSize, int ySize);
        Task DrawBoard(IBoard board, int xSize, int ySize);

        void ShowStatus(int level, int bombCount, int teleportCount, int robotCount, int moveCount, int maxLevel);

        //   Control GetControlAt(IntPoint p);
        void Clear(bool all);

        // void SetBoardSizeControls();
        Task PlaySound(Sound effect);

        Task Animate(BoardPosition bp, IntPoint oldPosition, IntPoint newPosition, uint millis);

        // void RemoveAnimate(BoardPosition position);
        void RemoveImage(string imageName);

        void ChangeImage(BoardPosition bp);
    }
}

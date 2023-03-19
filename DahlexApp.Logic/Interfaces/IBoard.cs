using DahlexApp.Logic.Game;
using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Interfaces;

public interface IBoard
{
    BoardPosition GetPosition(int x, int y);

    void SetPosition(int x, int y, BoardPosition pos);

    //void ResetPosition(int x, int y);
    int GetPositionHeight();

    int GetPositionWidth();

    IntPoint GetProfessor();

    BoardPosition GetTempPosition(int x, int y);

    void SetTempPosition(int x, int y, BoardPosition pos);

    void ResetTempPosition(int x, int y);

    IntPoint GetProfessorFromTemp();

    BoardPosition[,] TheBoard { get; set; }

    int GetRobotCount();
}
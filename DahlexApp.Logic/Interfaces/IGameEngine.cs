using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Interfaces;

public interface IGameEngine
{
    bool AreThereNoMoreLevels { get; }

    GameStatus Status { get; }

    int CurrentLevel { get; }

    Task StartGame(GameMode mode);

    // <summary>
    // Continue game from tombstone, called after setState
    // </summary>
    //Task ContinueGame(GameMode mode);

    /// <summary>
    /// Gather state to save to tombstone
    /// </summary>
    /// <param name="elapsed"></param>
    /// <returns></returns>
    IGameState GetState(TimeSpan elapsed);

    // <summary>
    // Restore state from tombstone, called before continueGame
    // </summary>
    // <param name="state"></param>
    //void SetState(IGameState state);

    Task StartNextLevel();

    Task MoveHeapsToTemp();

    Task<bool> MoveProfessorToTemp(MoveDirection dir);

    Task MoveRobotsToTemp();

    Task CommitTemp();

    Task<(bool Ok, int BombsLeft)> BlowBomb();

    Task<(bool Ok, int TelesLeft)> DoTeleport();

    IntPoint GetProfessorCoordinates();

    void AddHighScore(bool maxLevel);
}
﻿using System.Text.Json.Serialization;

namespace DahlexApp.Logic.Models;

public class HighScore
{
    public HighScore(string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, IntSize boardSize)
    {
        Name = name;
        Score = level;
        BombsLeft = bombsLeft;
        TeleportsLeft = teleportsLeft;
        Moves = moves;
        GameDuration = DateTime.Now - startTime;
        BoardSize = boardSize;
    }

    [JsonConstructor]
    public HighScore()
    {
    }

    public string Name { get; set; } = "";

    public int Score { get; set; }

    //public int Level
    //{
    //    get { return Score; }
    //}

    public int BombsLeft { get; }

    public int TeleportsLeft { get; }

    public int Moves { get; }

    public TimeSpan GameDuration { get; set; }

    public IntSize BoardSize { get; }

    [JsonIgnore]
    public string Content
    {
        get
        {
            if (Score <= 1)
            {
                return $"{Name}";
            }
            else
            {
                return $"{Name} reached level {Score} in {Math.Floor(GameDuration.TotalSeconds)}s";
            }
        }
    }

    public new string ToString()
    {
        return Content;
    }
}
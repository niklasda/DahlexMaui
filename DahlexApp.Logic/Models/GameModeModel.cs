namespace DahlexApp.Logic.Models;

public class GameModeModel
{
    public GameMode SelectedGameMode { get; set; }
}

public record struct IntPoint(int X, int Y)
{
    public static IntPoint Empty => new IntPoint(0, 0);

    public override string ToString()
    {
        return $"x:{X}, y:{Y}";
    }
}

public readonly record struct IntSize(int Width, int Height)
{
    public static IntSize Empty => new IntSize(0, 0);

    public override string ToString()
    {
        return $"w:{Width}, h:{Height}";
    }
}
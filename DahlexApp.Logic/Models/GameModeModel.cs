namespace DahlexApp.Logic.Models
{
    public class GameModeModel
    {
        public GameMode SelectedGameMode { get; set; }

    }

    public class IntPoint
    {
        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public static IntPoint Empty => new IntPoint(0, 0);

        public override string ToString()
        {
            return $"x:{X}, y:{Y}";
        }
    }

    public class IntSize
    {
        public IntSize(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public static IntSize Empty => new IntSize(0, 0);

        public override string ToString()
        {
            return $"w:{Width}, h:{Height}";
        }
    }
}

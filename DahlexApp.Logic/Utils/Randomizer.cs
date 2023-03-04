using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Utils;

public static class Randomizer
{
    private static readonly Random Randy = new Random(DateTime.Now.Millisecond);

    /// <summary>
    /// Returns a random point with
    /// 0 &lt;= x &lt; xMax and
    /// 0 &lt;= y &lt; yMax
    /// <seealso cref="Point"/>
    /// </summary>
    /// <param name="xMax">upper x bound (not inclusive)</param>
    /// <param name="yMax">upper y bound (not inclusive)</param>
    /// <returns></returns>
    public static IntPoint GetRandomPosition(int xMax, int yMax)
    {
        int x = Randy.Next(0, xMax);
        int y = Randy.Next(0, yMax);

        return new IntPoint(x, y);
    }

    public static string GetRandomFromSet(params string[] names)
    {
        if (names?.Length > 0)
        {
            int index = Randy.Next(0, names.Length); // upper limit is non-inclusive
            return names[index];
        }

        return string.Empty;
    }
}
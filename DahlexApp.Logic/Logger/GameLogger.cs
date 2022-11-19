using System.Diagnostics;

namespace DahlexApp.Logic.Logger;

public static class GameLogger
{
    public static string TheLog { get; private set; } = string.Empty;

    // private string txtLog;
    public static void AddLineToLog(string log)
    {
        Debug.WriteLine(log);
        string txtLog = TheLog;

        if (!string.IsNullOrEmpty(log))
        {
            log += Environment.NewLine;
            
            log += txtLog;
            TheLog = log;
        }
    }
}

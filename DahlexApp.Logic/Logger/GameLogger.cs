﻿using System.Diagnostics;

namespace DahlexApp.Logic.Logger
{
    public static class GameLogger
    {
       // private static string _theLog = string.Empty;

        //public static string TheLog
        //{
        //    get { return _theLog; }
        //    private set { _theLog = value; }
        //}

        // private string txtLog;
        public static void AddLineToLog(string log)
        {
            Debug.WriteLine(log);
      //      string txtLog = TheLog;
            //  if(!string.IsNullOrEmpty(txtLog.Text))
        //    log += Environment.NewLine;
          //  log += txtLog;
            //TheLog = log;
        }
    }
}

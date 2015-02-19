using System;
using System.IO;

namespace Vipr.CLI
{
    static class Logger
    {
        static internal void Log(string log)
        {
            Console.WriteLine(log);

            var logFile = Directory.GetCurrentDirectory() + "\\log.txt";
            File.AppendAllText(logFile, log + Environment.NewLine);
        }
    }
}
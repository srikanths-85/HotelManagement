using System;
using System.IO;
using System.Threading.Tasks.Dataflow;

namespace HotelManagement.Utilities
{
    public static class Logger
    {
        private static readonly string LogFilePath = "application.log";

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        public static void LogDebug(string message)
        {
            Log("DEBUG", message);
        }

        private static void Log(string level, string message)
        {
            try
            {
                using (var writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine("--------------------------------------------------------------------------------------");
                    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
                    writer.WriteLine(logMessage);
                }
            }
            catch
            {
                Console.WriteLine("Unable to write to log file.");
            }
        }
    }
}

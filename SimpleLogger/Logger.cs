using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class Logger
    {
        private static readonly string logDirectory = "Logs";
        private static readonly string logFilePath = Path.Combine(logDirectory, "log.txt");

        public static void Log(string message)
        {
            try
            {
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} ---> {message}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao escrever no log: {ex.Message}");
            }
        }
    }
}

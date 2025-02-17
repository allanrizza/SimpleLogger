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
        private static readonly string _logDirectory = "Logs";
        private static readonly string _logFilePath = Path.Combine(_logDirectory, "log.txt");

        public static void Log(string message)
        {
            try
            {
                CreateLogsDirectoryIfNotExists();
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} ---> {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao escrever no log: {ex.Message}");
            }
        }

        private static void CreateLogsDirectoryIfNotExists()
        {
            try
            {
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar diretório de Logs: {ex.Message}");
            }
        }
    }
}

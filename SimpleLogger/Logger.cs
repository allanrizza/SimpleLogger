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
        private const long MaxLogFileSize = 300 * 1024 * 1024;
        private static LogLevel _minLogLevel = LogLevel.DEBUG;

        public static void SetMinLogLevel(LogLevel level)
        {
            _minLogLevel = level;
        }

        public static void Log(LogLevel level, string message)
        {
            if (level < _minLogLevel)
                return;

            try
            {
                CreateLogsDirectoryIfNotExists();
                RotateLogIfNecessary();
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

        private static void RotateLogIfNecessary()
        {
            if(File.Exists(_logFilePath))
            {
                FileInfo logFileInfo = new FileInfo(_logFilePath);
                if(logFileInfo.Length >= MaxLogFileSize)
                {
                    string archiveName = Path.Combine(_logDirectory, $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
                    File.Move(_logFilePath, archiveName);
                }
            }
        }
    }
}

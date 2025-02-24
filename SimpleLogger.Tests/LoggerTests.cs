namespace SimpleLogger.Tests
{
    public class LoggerTests : IAsyncLifetime
    {
        private static readonly string _logDirectory = "Logs";
        private static readonly string _logFilePath = Path.Combine(_logDirectory, "log.txt");
        private const long MaxLogSize = 300 * 1024 * 1024; // 300MB
        private string[] _initialFiles;

        public async Task InitializeAsync()
        {
            // Salva os arquivos existentes antes dos testes
            if (Directory.Exists(_logDirectory))
                _initialFiles = Directory.GetFiles(_logDirectory);
            else
                _initialFiles = Array.Empty<string>();
        }

        public async Task DisposeAsync()
        {
            if (Directory.Exists(_logDirectory))
            {
                var filesAfterTest = Directory.GetFiles(_logDirectory);
                var testGeneratedFiles = filesAfterTest.Except(_initialFiles);

                foreach (var file in testGeneratedFiles)
                {
                    File.Delete(file);
                }

                // Se todos os arquivos da pasta foram gerados pelo teste, remove a pasta
                if (!Directory.GetFiles(_logDirectory).Any())
                {
                    Directory.Delete(_logDirectory);
                }
            }
        }

        [Fact]
        public void Log_CreatesLogDirectory_WhenNotExists()
        {
            //ARRANJO
            if (Directory.Exists(_logDirectory))
                Directory.Delete(_logDirectory, true);

            //AÇÃO
            Logger.Log(LogLevel.DEBUG, "Testando criação de diretório");

            //AFIRMAÇÃO
            Assert.True(Directory.Exists(_logDirectory));
        }

        [Fact]
        public void Log_CreatesLogFile_WhenLogIsCalled()
        {
            //ARRANJO
            if (File.Exists(_logFilePath))
                File.Delete(_logFilePath);

            //AÇÃO
            Logger.Log(LogLevel.DEBUG, "Testando a criação de arquivo de log");

            //AFIRMAÇÃO
            Assert.True(File.Exists(_logFilePath), "O arquivo de log não foi criado.");
        }

        [Fact]
        public void Log_WritesCorrectMessageToLogFile()
        {
            //ARRANJO
            string logMessage = "Mensagem de log de teste";
            Logger.Log(LogLevel.DEBUG, logMessage);

            //AÇÃO
            string logContent = File.ReadAllText(_logFilePath);

            //AFIRMAÇÃO
            Assert.Contains(logMessage, logContent);
        }

        [Fact]
        public void Log_ShouldRotate_WhenFileExceedsMaxSize()
        {
            //ARRANJO
            Directory.CreateDirectory(_logDirectory);
            File.WriteAllBytes(_logFilePath, new byte[MaxLogSize + 1]);

            //AÇÃO
            Logger.Log(LogLevel.DEBUG, "Teste de rotação por tamanho;");

            //AFIRMAÇÃO
            Assert.True(File.Exists(_logFilePath), "O novo arquivo de log não foi criado após a rotação.");
            var archivedLogs = Directory.GetFiles(_logDirectory, "log_*.txt");
            Assert.True(archivedLogs.Length > 0, "Nenhum arquivo de log foi arquivado corretamente.");
        }

        [Fact]
        public void Log_ShouldRespectMinLogLevel()
        {
            Logger.SetMinLogLevel(LogLevel.ERROR);
            Logger.Log(LogLevel.INFO, "This should be ignored");
            Logger.Log(LogLevel.ERROR, "This should be logged");

            string logContent = File.ReadAllText(_logFilePath);
            Assert.DoesNotContain("This should be ignored", logContent);
            Assert.Contains("This should be logged", logContent);
        }
    }
}
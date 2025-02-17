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

            //A��O
            Logger.Log("Testando cria��o de diret�rio");

            //AFIRMA��O
            Assert.True(Directory.Exists(_logDirectory));
        }

        [Fact]
        public void Log_CreatesLogFile_WhenLogIsCalled()
        {
            //ARRANJO
            if (File.Exists(_logFilePath))
                File.Delete(_logFilePath);

            //A��O
            Logger.Log("Testando a cria��o de arquivo de log");

            //AFIRMA��O
            Assert.True(File.Exists(_logFilePath), "O arquivo de log n�o foi criado.");
        }

        [Fact]
        public void Log_WritesCorrectMessageToLogFile()
        {
            //ARRANJO
            string logMessage = "Mensagem de log de teste";
            Logger.Log(logMessage);

            //A��O
            string logContent = File.ReadAllText(_logFilePath);

            //AFIRMA��O
            Assert.Contains(logMessage, logContent);
        }

        [Fact]
        public void Log_ShouldRotate_WhenFileExceedsMaxSize()
        {
            //ARRANJO
            Directory.CreateDirectory(_logDirectory);
            File.WriteAllBytes(_logFilePath, new byte[MaxLogSize + 1]);

            //A��O
            Logger.Log("Teste de rota��o por tamanho;");

            //AFIRMA��O
            Assert.True(File.Exists(_logFilePath), "O novo arquivo de log n�o foi criado ap�s a rota��o.");
            var archivedLogs = Directory.GetFiles(_logDirectory, "log_*.txt");
            Assert.True(archivedLogs.Length > 0, "Nenhum arquivo de log foi arquivado corretamente.");
        }
    }
}
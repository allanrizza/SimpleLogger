namespace SimpleLogger.Tests
{
    public class LoggerTests
    {
        private static readonly string logDirectory = "Logs";
        private static readonly string logFilePath = Path.Combine(logDirectory, "log.txt");
        public LoggerTests()
        {
            // Limpar o arquivo de log antes de cada teste (para garantir resultados consistentes)
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            // Limpar o diretório de logs (caso tenha sido criado por testes anteriores)
            if (Directory.Exists(logDirectory))
            {
                Directory.Delete(logDirectory, true);
            }
        }
        public void Dispose()
        {
        }

        [Fact]
        public void Log_CreatesLogDirectory_WhenNotExists()
        {
            // arranjo
            if(Directory.Exists(logDirectory))
                Directory.Delete(logDirectory, true);

            // ação
            Logger.Log("Testando criação de diretório");

            // afirmação
            Assert.True(Directory.Exists(logDirectory)); 
        }

        [Fact]
        public void Log_CreatesLogFile_WhenLogIsCalled()
        {
            //arranjo
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            //ação
            Logger.Log("Testando a criação de arquivo de log");

            //afirmação
            Assert.True(File.Exists(logFilePath), "O arquivo de log não foi criado.");
        }

        [Fact]
        public void Log_WritesCorrectMessageToLogFile()
        {
            //arranjo
            string logMessage = "Mensagem de log de teste";
            Logger.Log(logMessage);

            //ação
            string logContent = File.ReadAllText(logFilePath);

            //afirmação
            Assert.Contains(logMessage, logContent);
        }
    }
}